namespace CafeLinkAPI.Entities;

public class Coffee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Composition { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public CoffeeType? Type { get; set; }
}