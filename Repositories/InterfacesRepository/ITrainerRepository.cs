using MyGymProject.Server.Models;

namespace MyGymProject.Server.Repositories.Interfaces
{
    public interface ITrainerRepository
    {
        Task<IEnumerable<Trainer>> GetAllAsync();
        Task<Trainer?> GetByIdAsync(int id);
        Task<Trainer?> GetByLoginAsync(string login);
        Task<Trainer> AddAsync(Trainer trainer);
        Task<bool> UpdateAsync(Trainer trainer);
        Task<bool> DeleteAsync(int id);
    }
    
}
