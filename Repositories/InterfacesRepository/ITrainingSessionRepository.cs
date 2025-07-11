using MyGymProject.Server.Models;

namespace MyGymProject.Server.Repositories.InterfacesRepository
{
    public interface ITrainingSessionRepository
    {
        // Основные CRUD операции
        Task<IEnumerable<TrainingSession>> GetAllSessionsAsync();
        Task<TrainingSession?> GetTraining(int id);
        Task<TrainingSession> AddTraining(TrainingSession training);
        Task<bool> UpdateTraining(TrainingSession updatedTraining);
        Task<bool> DeleteTraining(int id);
        Task<bool> AddClientToSessionAsync(int sessionId, Client client);
        Task<bool> RemoveClientFromSessionAsync(int sessionId, int clientId);
        Task<IEnumerable<TrainingSession>> GetUpcomingSessionsAsync(DateTime fromDate, int trainingId);

        Task<IEnumerable<TrainingSession>> GetSessionsByTrainerIdAsync(int trainerId);
    }
}
