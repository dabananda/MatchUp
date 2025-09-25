using AutoMapper;
using MatchUp.DTOs;
using MatchUp.Entities;
using MatchUp.Extensions;

namespace MatchUp.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(d => d.Age, o => o.MapFrom( s => s.DateOfBirth.CalculateAge()))
                .ForMember(d => d.PhotoUrl, o => 
                o.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain)!.Url));

            CreateMap<Photo, PhotoDto>();
        }
    }
}