using MyGymProject.Server.Models;
using MyGymProject.Server.DTOs;
using MyGymProject.Server.DTOs.Training;

namespace MyGymProject.Server.Services.Interfaces
{
    public interface ITrainingService
    {
        Task<TrainingResponseDTO?> GetTraining(int id);
        Task<TrainingResponseDTO> CreateTraining(TrainingCreateDto dto);
        Task<bool> UpdateTraining(int id, TrainingCreateDto updatedDto);
        Task<bool> DeleteTraining(int id);
        Task<IEnumerable<TrainingResponseDTO>> GetScheduleForTrainerAsync(int trainerId);
        Task<bool> AddClientToTrainingAsync(int trainingId, int clientId);
        Task<bool> RemoveClientFromTrainingAsync(int trainingId, int clientId);
        Task<List<string>> GetClientsForTrainingAsync(int trainingId);
        Task<IEnumerable<TrainingResponseDTO>> GetAllTrainingsWithDetailsAsync();
        Task<IEnumerable<TrainingResponseDTO>> GetTrainingsByClient(int clientId);
        Task<IEnumerable<TrainingResponseDTO>> GetTrainingsByTrainerAndNameAsync(int trainerId, string trainingName);
    }
}
