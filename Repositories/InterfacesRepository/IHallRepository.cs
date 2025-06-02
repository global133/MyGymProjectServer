using MyGymProject.Server.Models;

namespace MyGymProject.Server.Repositories.Interfaces
{
    public interface IHallRepository
    {
        Task<IEnumerable<Hall?>> GetAllHalls();
        Task<Hall?> GetHall(int id);
        Task<Hall> AddHall(Hall hall);
        Task<bool> UpdateHall(int id, Hall updatedHall);
        Task<bool> DeleteHall(int id);
    }
}
