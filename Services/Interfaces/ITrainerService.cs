using MyGymProject.Server.DTOs.Trainer;
using MyGymProject.Server.Models;

namespace MyGymProject.Server.Services.Interfaces
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerReadDto>> GetAllAsync();
        Task<TrainerReadDto?> GetByIdAsync(int id);
        Task<Trainer?> GetByLoginAsync(string login);
        Task<TrainerReadDto?> AddAsync(TrainerCreateDto dto);
        Task<bool> UpdateAsync(int id, TrainerCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
