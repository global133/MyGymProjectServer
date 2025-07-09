using MyGymProject.Server.DTOs.TrainingSession;

namespace MyGymProject.Server.Services.Interfaces
{
    public interface ITrainingSessionService
    {
        // Основные операции
        Task<IEnumerable<TrainingSessionReadDto>> GetAllSessionsAsync();
        Task<TrainingSessionReadDto?> GetByIdAsync(int id);
        Task<IEnumerable<TrainingSessionReadDto>> GetByTrainingIdAsync(int trainingId);
        Task<TrainingSessionReadDto> CreateSessionAsync(int trainingId, TrainingSessionCreateDto dto); 
        Task<bool> UpdateSessionAsync(int id, TrainingSessionCreateDto dto);
        Task<bool> DeleteSessionAsync(int id);

        // Работа с клиентами
        Task<bool> AddClientToSessionAsync(int sessionId, int clientId);
        Task<bool> RemoveClientFromSessionAsync(int sessionId, int clientId);
        Task<bool> IsClientInSessionAsync(int sessionId, int clientId);

        // Проверки доступности
        Task<bool> IsSessionAvailableAsync(int sessionId);
        Task<int> GetAvailableSlotsAsync(int sessionId);

        // Получение расписания
        Task<IEnumerable<TrainingSessionReadDto>> GetUpcomingSessionsAsync(int trainingId);

        Task<IEnumerable<TrainingSessionReadDto>> GetWorkoutsByClientId(int clientId);
    }
}
