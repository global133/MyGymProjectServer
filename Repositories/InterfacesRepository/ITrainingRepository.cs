using MyGymProject.Server.Models;

namespace MyGymProject.Server.Repositories.Interfaces
{
    public interface ITrainingRepository
    {
        public Task<Training?> GetTraining(int id);
        public Task<Training> AddTraining(Training training);
        public Task<bool> UpdateTraining(int id, Training updatedTraining);
        public Task<bool> DeleteTraining(int id);
        Task<IEnumerable<Training>> GetScheduleForTrainerAsync(int trainerId);
        Task<bool> AddClientToTrainingAsync(int trainingId, int clientId);
        Task<bool> RemoveClientFromTrainingAsync(int trainingId, int clientId);
        Task<List<string>> GetClientsForTrainingAsync(int trainingId);
        Task<IEnumerable<Training>> GetAllTrainingsWithClientsAsync();
        Task<IEnumerable<Training>> GetTrainingsByClient(int clientId);
        Task<List<Training>> GetTrainingsByTrainerAndNameAsync(int trainerId, string trainingName);
    }
}
