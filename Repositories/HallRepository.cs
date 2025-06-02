using Microsoft.EntityFrameworkCore;
using MyGymProject.Server.Data;
using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories.Interfaces;

namespace MyGymProject.Server.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly DataBaseConnection _context;

        public HallRepository(DataBaseConnection context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Hall?>> GetAllHalls()
        {
            return await this._context.Halls.ToListAsync();
        }

        public async Task<Hall?> GetHall(int id)
        {
            return await this._context.Halls.FindAsync(id);
        }

        public async Task<Hall> AddHall(Hall hall)
        {
            await this._context.Halls.AddAsync(hall);
            await this._context.SaveChangesAsync();
            return hall;
        }

        public async Task<bool> UpdateHall(int id, Hall updatedHall)
        {
            var existing = await this._context.Halls.FindAsync(id);
            if (existing == null)
                return false;

            existing.Name = updatedHall.Name;
            existing.Description = updatedHall.Description;
            existing.Trainings = updatedHall.Trainings;

            await this._context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteHall(int id)
        {
            var hall = await this._context.Halls.FindAsync(id);
            if (hall == null)
                return false;

            this._context.Halls.Remove(hall);
            await this._context.SaveChangesAsync();
            return true;
        }
    }
}
