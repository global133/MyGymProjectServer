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
            CreateMap<TrainingSession, TrainingSessionDto>()
                .ForMember(dest => dest.BookedClients,
                           opt => opt.MapFrom(src => src.Clients)); 

            CreateMap<Client, ClientReadDto>();
        }
    }
}
