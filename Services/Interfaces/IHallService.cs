using MyGymProject.Server.DTOs.Hall;
using MyGymProject.Server.Models;

namespace MyGymProject.Server.Services.Interfaces
{
    public interface IHallService
    {
        Task<IEnumerable<HallDtoResponse>> GetAllHalls();
        Task<HallDtoResponse?> GetHall(int id);
        Task<HallDtoResponse> CreateHall(HallCreateDto dto);
        Task<bool> UpdateHall(int id, HallCreateDto dto);
        Task<bool> DeleteHall(int id);
    }
}
