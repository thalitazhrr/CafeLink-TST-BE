namespace CafeLinkAPI.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public required string Email { get; set; }
        
        public required byte[] PasswordHash { get; set; }

        public required byte[] PasswordSalt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}}