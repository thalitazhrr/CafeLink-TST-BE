using Microsoft.AspNetCore.Mvc;

namespace CafeLinkAPI.DTOs;

public class CafeSearchDto
{
    [FromQuery(Name = "coffee_type")]
    public string? CoffeeType { get; set; }
    [FromQuery(Name = "cafe_type")]
    public string? CafeType { get; set; }
    [FromQuery(Name = "location")]
    public string? Location { get; set; }
    [FromQuery(Name = "price_min")]
    public int PriceMin { get; set; }
    [FromQuery(Name = "price_max")]
    public int PriceMax { get; set; }
}