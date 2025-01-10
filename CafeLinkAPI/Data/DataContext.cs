using CafeLinkAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeLinkAPI.Data;


public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CafeType>()
            .HasMany(c => c.Cafes)
            .WithMany(t => t.Types);
    }
    public required DbSet<Account> Accounts { get; set; }
    public required DbSet<CoffeeType> CoffeeTypes { get; set; }
    public required DbSet<Coffee> Coffees { get; set; }
    public required DbSet<CafeType> CafeTypes { get; set; }
    public required DbSet<Cafe> Cafes { get; set; }
    public required DbSet<LikedCafe> LikedCafes { get; set; }
}