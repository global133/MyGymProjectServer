using MyGymProject.Server.Models;

namespace MyGymProject.Server.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByLoginAsync(string login);
        Task<Client?> GetByIdAsync(int id);
        Task<Client?> GetClientByPhoneAsync(string phone);
        Task AddAsync(Client client);
        Task <bool>UpdateAsync(Client client);
        Task <bool>DeleteAsync(int id);
        Task <bool> AddTrainingToClientAsync(int clientId, Training training);
    }
}
