using System.Text.Json.Serialization;

namespace CafeLinkAPI.DTOs
{
    public class GoogleUserInfoDto
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; }
        public string? Sub { get; set; }
        [JsonPropertyName("given_name")]
        public string? GivenName { get; set; }
        [JsonPropertyName("family_name")]
        public string? FamilyName { get; set; }

    }
}