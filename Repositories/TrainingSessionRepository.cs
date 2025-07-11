using Microsoft.EntityFrameworkCore;
using MyGymProject.Server.Data;
using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories.InterfacesRepository;
using System;

namespace MyGymProject.Server.Repositories
{
    public class TrainingSessionRepository : ITrainingSessionRepository
    {
        private readonly DataBaseConnection _context;

        public TrainingSessionRepository(DataBaseConnection context)
        {
            _context = context;
        }

        public async Task<TrainingSession?> GetTraining(int id)
        {
            return await _context.TrainingSessions
                .Include(ts => ts.Trainer)
                .Include(ts => ts.Hall)
                .Include(ts => ts.Clients)
                .FirstOrDefaultAsync(ts => ts.Id == id);
        }

        public async Task<TrainingSession> AddTraining(TrainingSession training)
        {
            training.Trainer = await _context.Trainers.FindAsync(training.Trainer.Id)
                                ?? throw new Exception("Trainer not found");
            training.Hall = await _context.Halls.FindAsync(training.Hall.Id)
                            ?? throw new Exception("Hall not found");

            await _context.TrainingSessions.AddAsync(training);
            await _context.SaveChangesAsync();
            return training;
        }

        public async Task<bool> UpdateTraining(TrainingSession session)
        {
            _context.TrainingSessions.Update(session);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTraining(int id)
        {
            var training = await _context.TrainingSessions.FindAsync(id);
            if (training == null)
                return false;

            _context.TrainingSessions.Remove(training);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<TrainingSession>> GetAllSessionsAsync()
        {
            return await _context.TrainingSessions
                .Include(ts => ts.Trainer)
                .Include(ts => ts.Hall)
                .Include(ts => ts.Clients)
                .OrderBy(ts => ts.StartTime)
                .ToListAsync();
        }

        public async Task<bool> AddClientToSessionAsync(int sessionId, Client client)
        {
            var session = await _context.TrainingSessions
                .Include(ts => ts.Clients)
                .FirstOrDefaultAsync(ts => ts.Id == sessionId);

            if (session == null)
                return false;

            var existingClient = await _context.Clients.FindAsync(client.Id);
            if (existingClient == null || session.Clients.Any(c => c.Id == client.Id))
                return false;

            session.Clients.Add(existingClient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveClientFromSessionAsync(int sessionId, int clientId)
        {
            var session = await _context.TrainingSessions
                .Include(ts => ts.Clients)
                .FirstOrDefaultAsync(ts => ts.Id == sessionId);

            var client = session?.Clients.FirstOrDefault(c => c.Id == clientId);
            if (client == null) return false;

            session.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<TrainingSession>> GetUpcomingSessionsAsync(DateTime fromDate, int trainingId)
        {
            return await _context.TrainingSessions
                .Where(ts => ts.Id == trainingId && ts.StartTime >= fromDate)
                .Include(ts => ts.Clients)
                .Include(ts => ts.Trainer)
                .Include(ts => ts.Hall)
                .OrderBy(ts => ts.StartTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingSession>> GetSessionsByTrainerIdAsync(int trainerId)
        {
            return await _context.TrainingSessions
                .Where(ts => ts.Trainer.Id == trainerId)
                .Include(ts => ts.Clients)
                .Include(ts => ts.Hall)
                .OrderBy(ts => ts.StartTime)
                .ToListAsync();
        }
    }
}
