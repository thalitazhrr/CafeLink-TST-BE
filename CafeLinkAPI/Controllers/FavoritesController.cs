using CafeLinkAPI.Data;
using CafeLinkAPI.DTOs;
using CafeLinkAPI.Entities;
using CafeLinkAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace CafeLinkAPI.Controllers;

[Authorize]
public class FavoritesController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ILogger<FavoritesController> _logger;
    public FavoritesController(DataContext context, ILogger<FavoritesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LikedCafe>>> GetFavorites()
    {
        _logger.LogInformation("Getting favorites");
        var userId = User.GetUserId();
        var favorites = await _context.LikedCafes
            .Include(c => c.Cafe)
            .Where(c => c.AccountId == userId)
            .ToListAsync();
        return Ok(favorites);

    }
    [HttpGet("check/{cafeId}")]
    public async Task<ActionResult<bool>> CheckFavorite(int cafeId)
    {
        _logger.LogInformation("Checking favorite");
        var userId = User.GetUserId();
        var existingFavorite = await _context.LikedCafes
            .Where(c => c.AccountId == userId && c.CafeId == cafeId)
            .FirstOrDefaultAsync();
        return Ok(existingFavorite != null);
    }

    [HttpPost("{cafeId}")]
    public async Task<ActionResult> AddFavorite(int cafeId)
    {
        _context.Database.BeginTransaction();
        try {
            _logger.LogInformation("Adding favorite");
            var userId = User.GetUserId();
            var existingFavorite = await _context.LikedCafes
                .Where(c => c.AccountId == userId && c.CafeId == cafeId)
                .FirstOrDefaultAsync();
            if (existingFavorite != null)
            {
                return BadRequest("Cafe already favorited");
            }
            Account? user = await _context.Accounts.FindAsync(userId);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            Cafe? cafe = await _context.Cafes.FindAsync(cafeId);
            if (cafe == null)
            {
                return BadRequest("Cafe not found");
            }
            cafe.LikeCount++;
            _context.Cafes.Update(cafe);
            
            var newFavorite = new LikedCafe
            {
                AccountId = userId,
                Account = user,
                CafeId = cafeId,
                Cafe = cafe
            };
            _context.LikedCafes.Add(newFavorite);
            await _context.SaveChangesAsync();
            _context.Database.CommitTransaction();
            return Ok();
        } catch (Exception e) {
            _logger.LogError(e, "Error adding favorite");
            _context.Database.RollbackTransaction();
            return BadRequest("Error adding favorite");
        }
    }
    [HttpDelete("{cafeId}")]
    public async Task<ActionResult> RemoveFavorite(int cafeId)
    {
        _context.Database.BeginTransaction();
        try {
            _logger.LogInformation("Removing favorite");
            var userId = User.GetUserId();
            var existingFavorite = await _context.LikedCafes
                .Where(c => c.AccountId == userId && c.CafeId == cafeId)
                .FirstOrDefaultAsync();
            if (existingFavorite != null)
            {
                Cafe? cafe = await _context.Cafes.FindAsync(cafeId);
                if (cafe != null)
                {
                    cafe.LikeCount--;
                    _context.Cafes.Update(cafe);
                }
                _context.LikedCafes.Remove(existingFavorite);
                await _context.SaveChangesAsync();
            }
            _context.Database.CommitTransaction();
            return Ok();
        } catch (Exception e) {
            _logger.LogError(e, "Error removing favorite");
            _context.Database.RollbackTransaction();
            return BadRequest("Error removing favorite");
        }
    }

}   