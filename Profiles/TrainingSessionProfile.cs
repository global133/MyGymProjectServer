using AutoMapper;
using MyGymProject.Server.DTOs.Client;
using MyGymProject.Server.DTOs.TrainingSession;
using MyGymProject.Server.Models;

namespace MyGymProject.Server.Profiles
{
    public class TrainingSessionProfile : Profile
    {
        public TrainingSessionProfile()
        {
            CreateMap<TrainingSession, TrainingSessionReadDto>();
   

            CreateMap<TrainingSessionCreateDto, TrainingSession>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.StartTime, DateTimeKind.Utc)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EndTime, DateTimeKind.Utc)))
                .ForMember(dest => dest.Clients, opt => opt.Ignore()); 

            CreateMap<Client, ClientReadDto>();
        }
    }
}
