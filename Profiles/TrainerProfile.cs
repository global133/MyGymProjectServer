using MyGymProject.Server.DTOs.Trainer;
using MyGymProject.Server.Models;
using AutoMapper;

namespace MyGymProject.Server.Profiles
{
    public class TrainerProfile : Profile
    {
        public TrainerProfile()
        {
            CreateMap<TrainerCreateDto, Trainer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Trainings, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.DateOfBirth, DateTimeKind.Utc)));

            CreateMap<Trainer, TrainerReadDto>();
        }
    }
}
