using Microsoft.EntityFrameworkCore;
using MyGymProject.Server.Data;
using MyGymProject.Server.DTOs.Training;
using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories.Interfaces;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace MyGymProject.Server.Repositories
{
    public class TrainingRepository: ITrainingRepository
    {
        private readonly DataBaseConnection _context;
        public TrainingRepository(DataBaseConnection dataBaseConnection)
        {
            this._context = dataBaseConnection;
        }

        public async Task<Training?> GetTraining (int id)
        {
            return await this._context.Trainings.FindAsync(id);
        }

        public async Task<Training> AddTraining (Training training)
        {
            await this._context.Trainings.AddAsync(training);
            await this._context.SaveChangesAsync();
            return training;
        }

        public async Task<bool> UpdateTraining (int id, Training updatedTraining)
        {
            var existing = await this._context.Trainings
            .Include(t => t.Clients)
            .Include(t => t.Trainer)
            .Include(t => t.Hall)
            .FirstOrDefaultAsync(t => t.Id == id);

            if (existing == null) return false;

            existing.IsGroup = updatedTraining.IsGroup;
            existing.Time = updatedTraining.Time;

            existing.Trainer = await this._context.Trainers.FindAsync(updatedTraining.Trainer.Id);
            existing.Hall = await this._context.Halls.FindAsync(updatedTraining.Hall.Id);

            existing.Clients.Clear();
            foreach (var client in updatedTraining.Clients)
            {
                var existingClient = await this._context.Clients.FindAsync(client.Id);
                if (existingClient != null)
                {
                    existing.Clients.Add(existingClient);
                }
            }

            await this._context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTraining (int id)
        {
            var training = await _context.Trainings.FirstOrDefaultAsync(t => t.Id == id);

            if (training == null)
                return false;

            this._context.Trainings.Remove(training);
            await this._context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<Training>> GetScheduleForTrainerAsync(int trainerId)
        {
            return await this._context.Trainings
                .Include(t => t.Trainer)
                .Include(t => t.Hall)
                .Include(t => t.Clients)
                .Where(t => t.Trainer.Id == trainerId)
                .ToListAsync();
        }

        public async Task<bool> AddClientToTrainingAsync(int trainingId, int clientId)
        {
            var training = await this._context.Trainings
                .Include(t => t.Clients)
                .FirstOrDefaultAsync(t => t.Id == trainingId);
            if (training == null) return false;

            var client = await this._context.Clients.FindAsync(clientId);

            if (client == null) return false;

            if (training.Clients.Any(c => c.Id == clientId))
                return false;

            if (!training.IsGroup && training.Clients.Count >= 1)
                return false;

            training.Clients.Add(client);
            await this._context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveClientFromTrainingAsync(int trainingId, int clientId)
        {
            var training = await _context.Trainings
                .Include(t => t.Clients)
                .FirstOrDefaultAsync(t => t.Id == trainingId);

            if (training == null)
                return false;

            var client = training.Clients.FirstOrDefault(c => c.Id == clientId);
            if (client == null)
                return false;

            training.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>> GetClientsForTrainingAsync(int trainingId)
        {
            var training = await _context.Trainings
                .Include(t => t.Clients)
                .FirstOrDefaultAsync(t => t.Id == trainingId);

            if (training == null || training.Clients == null)
                return new List<string>();

            return training.Clients.Select(c => c.FullName).ToList();
        }

        public async Task<IEnumerable<Training>> GetAllTrainingsWithClientsAsync()
        {
            return await _context.Trainings
                .Include(t => t.Clients)  
                .Include(t => t.Trainer)  
                .Include(t => t.Hall)     
                .ToListAsync();
        }

        public async Task<IEnumerable<Training>> GetTrainingsByClient(int clientId)
        {
            return await _context.Trainings
                .Include(t => t.Trainer)
                .Include(t => t.Hall)
                .Include(t => t.Clients)
                .Where(t => t.Clients.Any(c => c.Id == clientId))
                .ToListAsync();
        }

        public async Task<List<Training>> GetTrainingsByTrainerAndNameAsync(int trainerId, string trainingName)
        {
            return await _context.Trainings
                .Include(t => t.Clients)         
                .Include(t => t.Hall)       
                .Where(t =>
                    t.Trainer.Id == trainerId &&
                    t.Name.Contains(trainingName))
                .ToListAsync();
        }
    }
}
