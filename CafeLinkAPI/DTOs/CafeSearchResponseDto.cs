using CafeLinkAPI.Entities;

namespace  CafeLinkAPI.DTOs;

public class CafeSearchResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public List<CafeType>? Types { get; set; }
    public List<Coffee>? Coffees { get; set; }
    public int PriceAverage { get; set; }

    public string? DistrictAddress { get; set; }
    public string? CityAddress { get; set; }
    public string? ProvinceAddress { get; set; }    
    public string? FullAddress { get; set; }
    public List<string>? Specials { get; set; }
    public int LikeCount { get; set; } = 0;
    public bool IsLiked { get; set; } = false;
}