namespace CafeLinkAPI.Entities;

public class LikedCafe
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public required Account Account { get; set; }
    public int CafeId { get; set; }
    public required Cafe Cafe { get; set; }
}