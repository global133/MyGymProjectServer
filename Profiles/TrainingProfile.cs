using MyGymProject.Server.DTOs.Training;
using MyGymProject.Server.Models;
using AutoMapper;

namespace MyGymProject.Server.Profiles
{
    public class TrainingProfile: Profile
    {
        public TrainingProfile()
        {
            CreateMap<TrainingCreateDto, Training>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Trainer, opt => opt.Ignore())
                .ForMember(dest => dest.Hall, opt => opt.Ignore())
                .ForMember(dest => dest.Clients, opt => opt.Ignore())
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Time, DateTimeKind.Utc)));

            CreateMap<Training, TrainingResponseDTO>()
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer != null ? $"{src.Trainer.FullName}": ""))
                .ForMember(dest => dest.HallName, opt => opt.MapFrom(src => src.Hall != null ? src.Hall.Name : ""))
                .ForMember(dest => dest.ClientNames, opt => opt.MapFrom(src => src.Clients.Select(c => $"{c.FullName}").ToList()));
        }
    }
}
