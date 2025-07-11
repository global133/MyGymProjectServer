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
            CreateMap<TrainingSession, TrainingSessionReadDto>()
                .ForMember(dest => dest.TrainingName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.FullName))
                .ForMember(dest => dest.Clients, opt => opt.MapFrom(src => src.Clients));

            CreateMap<TrainingSessionCreateDto, TrainingSession>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.StartTime, DateTimeKind.Utc)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EndTime, DateTimeKind.Utc)))
                .ForMember(dest => dest.Clients, opt => opt.Ignore()) 
                .ForMember(dest => dest.Trainer, opt => opt.Ignore())
                .ForMember(dest => dest.Hall, opt => opt.Ignore());

            CreateMap<Client, ClientReadDto>();
        }
    }
}
