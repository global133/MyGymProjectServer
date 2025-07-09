using MyGymProject.Server.Models;

namespace MyGymProject.Server.Repositories.InterfacesRepository
{
    public interface ITrainingSessionRepository
    {
        // Основные CRUD операции
        Task<IEnumerable<TrainingSession>> GetAllSessionsAsync();
        Task<TrainingSession?> GetByIdAsync(int id);
        Task<IEnumerable<TrainingSession>> GetByTrainingIdAsync(int trainingId);
        Task<TrainingSession> AddAsync(TrainingSession session);
        Task UpdateAsync(TrainingSession session);
        Task DeleteAsync(int id);

        // Специфичные методы для работы с клиентами
        Task<int> GetClientsCountAsync(int sessionId);
        Task<bool> AddClientToSessionAsync(int sessionId, Client client);
        Task<bool> RemoveClientFromSessionAsync(int sessionId, int clientId);
        Task<bool> IsClientInSessionAsync(int sessionId, int clientId);

        // Методы для расписания
        Task<IEnumerable<TrainingSession>> GetUpcomingSessionsAsync(DateTime fromDate, int trainingId);
    }
}
