using System.ComponentModel.DataAnnotations;
namespace CafeLinkAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(maximumLength: 64, MinimumLength = 5)]
        public required string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 64, MinimumLength = 5)]
        public required string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 32, MinimumLength = 6)]
        public required string Password { get; set; }
    }
}