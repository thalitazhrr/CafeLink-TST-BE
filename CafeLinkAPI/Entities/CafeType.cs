namespace CafeLinkAPI.Entities;

public class CafeType
{
    public int Id { get; set; }
    public string? Name { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public List<Cafe>? Cafes { get; set; }
}