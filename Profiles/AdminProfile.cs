using MyGymProject.Server.DTOs.Admin;
using MyGymProject.Server.Models;
using AutoMapper;

namespace MyGymProject.Server.Profiles
{
    public class AdminProfile: Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminCreateDto, Admin>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.DateOfBirth, DateTimeKind.Utc)));

            CreateMap<Admin, AdminReadDto>();

        }
    }
}
