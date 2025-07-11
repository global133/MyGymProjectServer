using MyGymProject.Server.DTOs.TrainingSession;

namespace MyGymProject.Server.Services.Interfaces
{
    public interface ITrainingSessionService
    {
        // Основные операции
        Task<IEnumerable<TrainingSessionReadDto>> GetAllSessionsAsync();
        Task<TrainingSessionReadDto?> GetByIdAsync(int id);
        Task<TrainingSessionReadDto?> CreateSessionAsync(TrainingSessionCreateDto dto);
        Task<bool> UpdateSessionAsync(int id, TrainingSessionCreateDto dto);
        Task<bool> DeleteSessionAsync(int id);

        // Работа с клиентами
        Task<bool> AddClientToSessionAsync(int sessionId, int clientId);
        Task<bool> RemoveClientFromSessionAsync(int sessionId, int clientId);
        Task<IEnumerable<TrainingSessionReadDto>> GetUpcomingSessionsAsync(int trainingId);

        Task<IEnumerable<TrainingSessionReadDto>> GetSessionsByTrainerAsync(int trainerId);
    }
}
