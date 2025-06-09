using MyGymProject.Server.DTOs.Client;
using MyGymProject.Server.Models;

namespace MyGymProject.Server.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientReadDto>> GetAllAsync();
        Task<ClientReadDto?> GetByIdAsync(int id);
        Task<Client?> GetByLoginAsync(string login);
        Task<bool> AddAsync(ClientCreateDto createDto);
        Task<bool> UpdateAsync(int id, ClientUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> AddTrainingAsync(int clientId, Training training);
    }
}
