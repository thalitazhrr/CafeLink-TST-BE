using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using CafeLinkAPI.Data;
using CafeLinkAPI.DTOs;
using CafeLinkAPI.Entities;
using CafeLinkAPI.Interfaces;
using CafeLinkAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using System.Net.Http.Headers;

namespace CafeLinkAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(DataContext context, ITokenService tokenService, ILogger<AccountController> logger)
        {
            _tokenService = tokenService;
            _context = context;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();
            string Email = registerDto.Email.ToLower();
            string Name = registerDto.Name;
            string password = registerDto.Password;
            if (await AccountExists(Email))
                return BadRequest("Email is Registered");
            if (!EmailValid(Email))
                return BadRequest("Invalid Email");

            var user = new Account
            {
                Email = Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key,
                Name = Name,
            };

            _context.Accounts.Add(user);
            
            await _context.SaveChangesAsync();

            return Ok
            (
                new UserDto
                    {
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user),
                        Name = user.Name ?? "",
                    }
            );
        }

        // Login Endpoint here

        // Buat DTO Login isinya sama kaya register, tapi dibedain aja karena fungsinya beda.
        // Cari User dengan, var user = _context.Account.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
        // passwordnya dengan di hash dulu, baru dibandingin. contoh
        // using var hmac = new HMACSHA512 (user.PasswordSalt);
        // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        // atau boleh buat repository untuk Account.
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
            string email = loginDto.Email.ToLower();
            string password = loginDto.Password;

            // how to get email and password from database? belum tau yang ini caranya gimana
            Account? user = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if (user == null)
                return BadRequest("Email is not registered");

            using var hmac = new HMACSHA512 (user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            if (computedHash.Length != user.PasswordHash.Length || computedHash.Where((t, i) => t != user.PasswordHash[i]).Any())
                return BadRequest("Password does not match");
            return Ok(new UserDto {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Name = user.Name ?? "",
            });
        } 
        [HttpPost("Sso")]
        public async Task<ActionResult<UserDto>> LoginWithGoogleCode(GoogleTokenDto googleTokenDto){
            var code = googleTokenDto.Code;
            using HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.googleapis.com/oauth2/v3/userinfo");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", code);
            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return BadRequest("Invalid Google Code");
            var payload = await response.Content.ReadFromJsonAsync<GoogleUserInfoDto>();
            if (payload == null)
                return BadRequest("Invalid Google Code");
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == payload.Email);
            if (user == null)
                user = await CreateAccount(payload);
            if (user == null)
                return BadRequest("Failed to create account");
            
            return Ok(new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Name = user.Name ?? "",
            });
        }
        private Task<Account?> CreateAccount(GoogleUserInfoDto payload)
        {
            using var hmac = new HMACSHA512();
            var password = Guid.NewGuid().ToString();
            if (payload.Email == null)
                return Task.FromResult<Account?>(null);
            var user = new Account
            {
                Email = payload.Email,
                Name = payload.Name,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key,
                
            };
            _context.Accounts.Add(user);
            _context.SaveChangesAsync();
            return Task.FromResult<Account?>(user);
        }
        private async Task<bool> AccountExists(string Email)
        {
            return await _context.Accounts.AnyAsync(x => x.Email == Email);
        }
        private bool EmailValid(string Email)
        {
            //check Email for valid using regex
            string pattern = @"^[\w-]+@([\w-]+\.)+[\w-]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(Email);
        }

    }
}