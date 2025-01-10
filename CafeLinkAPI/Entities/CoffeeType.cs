namespace CafeLinkAPI.Entities;

public class CoffeeType
{
    public int Id { get; set; }
    public string? Name { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}