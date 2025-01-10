using CafeLinkAPI.Data;
using CafeLinkAPI.DTOs;
using CafeLinkAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace CafeLinkAPI.Controllers;
using AutoMapper;
using CafeLinkAPI.Extensions;

[Authorize]
public class CafeController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ILogger<CafeController> _logger;
    private readonly IMapper _mapper;
    public CafeController(DataContext context, ILogger<CafeController> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }
    [HttpGet("cafe-types")]
    public async Task<ActionResult<IEnumerable<CafeType>>> GetCafeTypes()
    {
        _logger.LogInformation("Getting cafe types");
        var cafeTypes = await _context.CafeTypes.ToListAsync();
        return Ok(cafeTypes);
    }
    [HttpGet("coffee-types")]
    public async Task<ActionResult<IEnumerable<CoffeeType>>> GetCoffeeTypes()
    {
        _logger.LogInformation("Getting coffee types");
        var coffeeTypes = await _context.CoffeeTypes.ToListAsync();
        return Ok(coffeeTypes);
    }
    [AllowAnonymous]
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Cafe>>> GetCafes()
    {
        _logger.LogInformation("Getting cafes");
        var cafes = await _context.Cafes
            .Include(c => c.Types)
            .ToListAsync();
        return Ok(cafes);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CafeSearchResponseDto>>> SearchCafe(CafeSearchDto dto)
    {
        _logger.LogInformation("Searching for cafes");
        _logger.LogInformation($"CoffeeType: {dto.CoffeeType}\nCafeType: {dto.CafeType}\nLocation: {dto.Location}\nPriceMin: {dto.PriceMin}\nPriceMax: {dto.PriceMax}");
        var cafeQuery = _context.Cafes
            .Include(c => c.Coffees)
            .ThenInclude(c => c.Type)
            .Include(c => c.Types)
            .AsQueryable();

        if (dto.PriceMin > 0)
        {
            cafeQuery = cafeQuery.Where(c => c.PriceAverage >= dto.PriceMin);
        }
        if (dto.PriceMax > 0)
        {
            cafeQuery = cafeQuery.Where(c => c.PriceAverage <= dto.PriceMax);
        }

        if (!string.IsNullOrEmpty(dto.CoffeeType))
        {
            cafeQuery = cafeQuery.Where(c => c.Coffees.Any(coffee => coffee.Type.Name.ToLower() == dto.CoffeeType.ToLower()));
        }
        if (!string.IsNullOrEmpty(dto.CafeType))
        {
            cafeQuery = cafeQuery.Where(c => c.Types.Any(type => type.Name.ToLower() == dto.CafeType.ToLower()));
        }
        if (!string.IsNullOrEmpty(dto.Location))
        {
            cafeQuery = cafeQuery.Where(c => c.FullAddress.ToLower().Contains(dto.Location.ToLower()));
        }
        var cafes = await cafeQuery.ToListAsync();
        var cafeDtos = _mapper.Map<List<CafeSearchResponseDto>>(cafes);
        for (int i = 0; i < cafes.Count; i++)
        {
            cafeDtos[i].IsLiked = await _context.LikedCafes
                .Where(c => c.CafeId == cafes[i].Id && c.AccountId == User.GetUserId())
                .FirstOrDefaultAsync() != null;
        }
        cafeDtos = cafeDtos.OrderByDescending(c => c.IsLiked).ThenByDescending( c => c.LikeCount).ToList();
        return Ok(cafeDtos);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Cafe>> GetCafe(int id)
    {
        _logger.LogInformation($"Getting cafe with id {id}");
        var cafe = await _context.Cafes
            .Include(c => c.Coffees)
            .ThenInclude(c => c.Type)
            .Include(c => c.Types)
            .FirstOrDefaultAsync(c => c.Id == id);
        return Ok(cafe);
    }
    [AllowAnonymous]
    [HttpGet("coffee/uniques")]
    public async Task<ActionResult<IEnumerable<Coffee>>> GetUniqueCoffee()
    {
        _logger.LogInformation("Getting unique coffee types");
        var coffee = await _context.Coffees
            .Include(c => c.Type)
            .ToListAsync();
        coffee = coffee.GroupBy(c => c.Name).Select(c => c.First()).ToList();
        return Ok(coffee);
    }
};