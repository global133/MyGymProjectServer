using MyGymProject.Server.Data;
using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace MyGymProject.Server.Repositories
{
    public class TrainerRepository: ITrainerRepository
    {
        private readonly DataBaseConnection _context;

        public TrainerRepository(DataBaseConnection context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Trainer>> GetAllAsync()
        {
            return await this._context.Trainers.ToListAsync();
        }

        public async Task<Trainer?> GetByIdAsync(int id)
        {
            return await this._context.Trainers.FindAsync(id);
        }

        public async Task<Trainer?> GetByLoginAsync(string login)
        {
            return await this._context.Trainers.FirstOrDefaultAsync(t => t.Login == login);
        }

        public async Task<Trainer> AddAsync(Trainer trainer)
        {
            try
            {
                await this._context.Trainers.AddAsync(trainer);
                await this._context.SaveChangesAsync();
                return trainer;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Trainer trainer)
        {
            var existing = await this._context.Trainers.FindAsync(trainer.Id);
            if (existing == null)
                return false;

            this._context.Entry(existing).CurrentValues.SetValues(trainer);
            await this._context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trainer = await this._context.Trainers.FindAsync(id);
            if (trainer == null)
                return false;

            this._context.Trainers.Remove(trainer);
            await this._context.SaveChangesAsync();
            return true;
        }
    }
}
