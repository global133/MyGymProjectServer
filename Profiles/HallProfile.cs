using MyGymProject.Server.DTOs.Hall;
using MyGymProject.Server.Models;
using AutoMapper;

namespace MyGymProject.Server.Profiles
{
    public class HallProfile : Profile
    {
        public HallProfile()
        {
            CreateMap<HallCreateDto, Hall>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Trainings, opt => opt.Ignore());

            CreateMap<Hall, HallDtoResponse>();
        }
    }
}
