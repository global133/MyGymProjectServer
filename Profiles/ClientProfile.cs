using MyGymProject.Server.DTOs.Client;
using MyGymProject.Server.Models;
using AutoMapper;

namespace MyGymProject.Server.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientCreateDto, Client>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Trainings, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.DateOfBirth, DateTimeKind.Utc)));

            CreateMap<Client, ClientReadDto>();
        }
    }
}
