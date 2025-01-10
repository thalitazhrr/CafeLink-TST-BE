using System.ComponentModel.DataAnnotations;
namespace CafeLinkAPI.DTOs
{
    public class PackageSearchDto
    {
        public string? Name { get; set; }
        public string? Location { get; set; }

        public int? MinPrice { get; set; }

        public int? MaxPrice { get; set; }

        public string? Activity { get; set; }

        public int? MinParticipantLimit { get; set; }

        public int? MaxParticipantLimit { get; set; }

        public DateOnly? DataRangeStart { get; set; }

        public DateOnly? DataRangeEnd { get; set; }

        public TimeOnly? ActivityTimeStart { get; set; }

        public TimeOnly? ActivityTimeEnd { get; set; }

        public int? MinDifficulty { get; set; }

        public int? MaxDifficulty { get; set; }
    }
}