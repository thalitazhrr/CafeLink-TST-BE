
using AutoMapper;
using CafeLinkAPI.DTOs;
using CafeLinkAPI.Entities;

namespace CafeLinkAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cafe, CafeSearchResponseDto>()
                .ForMember(dest => dest.IsLiked, opt => opt.Ignore());
        }
    }
}