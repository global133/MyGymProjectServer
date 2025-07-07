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

        public async Task<IEnumerable<TrainingSession>> GetAllSessionsAsync()
        {
            return await _context.TrainingSessions
                .Include(ts => ts.Training)
                .Include(ts => ts.Clients)
                .OrderBy(ts => ts.StartTime)
                .ToListAsync();
        }
        public async Task<TrainingSession?> GetByIdAsync(int id)
        {
            return await _context.TrainingSessions
                .Include(ts => ts.Training)
                .Include(ts => ts.Clients)
                .FirstOrDefaultAsync(ts => ts.Id == id);
        }

        public async Task<IEnumerable<TrainingSession>> GetByTrainingIdAsync(int trainingId)
        {
            return await _context.TrainingSessions
                .Where(ts => ts.Training.Id == trainingId)
                .Include(ts => ts.Clients)
                .OrderBy(ts => ts.StartTime)
                .ToListAsync();
        }

        public async Task<TrainingSession> AddAsync(TrainingSession session)
        {
            await _context.TrainingSessions.AddAsync(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task UpdateAsync(TrainingSession session)
        {
            _context.TrainingSessions.Update(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var session = await GetByIdAsync(id);
            if (session != null)
            {
                _context.TrainingSessions.Remove(session);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetClientsCountAsync(int sessionId)
        {
            var session = await _context.TrainingSessions
                .Include(ts => ts.Clients)
                .FirstOrDefaultAsync(ts => ts.Id == sessionId);

            return session?.Clients.Count ?? 0;
        }

        public async Task<bool> AddClientToSessionAsync(int sessionId, Client client)
        {
            var session = await _context.TrainingSessions
                .Include(ts => ts.Clients)
                .FirstOrDefaultAsync(ts => ts.Id == sessionId);

            if (session == null || session.Clients.Any(c => c.Id == client.Id))
                return false;

            session.Clients.Add(client);
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

        public async Task<bool> IsClientInSessionAsync(int sessionId, int clientId)
        {
            return await _context.TrainingSessions
                .Where(ts => ts.Id == sessionId)
                .SelectMany(ts => ts.Clients)
                .AnyAsync(c => c.Id == clientId);
        }

        public async Task<IEnumerable<TrainingSession>> GetUpcomingSessionsAsync(DateTime fromDate)
        {
            return await _context.TrainingSessions
                .Where(ts => ts.StartTime >= fromDate)
                .Include(ts => ts.Clients)
                .OrderBy(ts => ts.StartTime)
                .ToListAsync();
        }
    }
}
