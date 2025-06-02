using MyGymProject.Server.DTOs.Admin;
using MyGymProject.Server.DTOs.Client;
using MyGymProject.Server.DTOs.Hall;
using MyGymProject.Server.DTOs.Trainer;
using MyGymProject.Server.DTOs.Training;
using MyGymProject.Server.Models;

namespace MyGymProject.Server.Services.Interfaces
{
    public interface IAdminService
    {
        // Методы для управления админами
        Task<AdminReadDto?> GetAdminByloginAsync(string login);
        Task<AdminReadDto> CreateAdminAsync(AdminCreateDto dto);
        Task<IEnumerable<AdminReadDto>> GetAllAdminsAsync();
        Task<AdminReadDto?> GetAdminByIdAsync(int id);
        Task<bool> UpdateAdminAsync(int id, AdminCreateDto dto);
        Task<bool> DeleteAdminAsync(int id);

        // Методы для управления клиентами
        Task<IEnumerable<ClientReadDto>> GetAllUSersAsync();
        Task<ClientReadDto?> GetClientByIdAsync(int clientId);
        Task<bool> AddClientAsync(ClientCreateDto client);
        Task<bool> UpdateClientAsync(int clientId, ClientCreateDto updatedClient);
        Task<bool> DeleteClientAsync(int clientId);

        // Методы для управления тренерами
        Task<IEnumerable<TrainerReadDto>> GetAllTrainersWithTrainingsAsync();
        Task<TrainerReadDto?> GetTrainerByIdAsync(int trainerId);
        Task<TrainerReadDto> AddTrainerAsync(TrainerCreateDto trainer);
        Task<bool> UpdateTrainerAsync(int trainerId, TrainerCreateDto updatedTrainer);
        Task<bool> DeleteTrainerAsync(int trainerId);

        // Методы для управления тренировками
        Task<IEnumerable<TrainingResponseDTO>> GetAllTrainingsAsync();
        Task<TrainingResponseDTO?> GetTrainingByIdAsync(int trainingId);
        Task<TrainingResponseDTO> AddTrainingAsync(TrainingCreateDto training);
        Task<bool> UpdateTrainingAsync(int trainingId, TrainingCreateDto updatedTraining);
        Task<bool> DeleteTrainingAsync(int trainingId);

        // Методы для управления залами
        Task<IEnumerable<HallDtoResponse>> GetAllHallsAsync();
        Task<HallDtoResponse?> GetHallByIdAsync(int hallId);
        Task<HallDtoResponse> AddHallAsync(HallCreateDto hall);
        Task<bool> UpdateHallAsync(int hallId, HallCreateDto updatedHall);
        Task<bool> DeleteHallAsync(int hallId);
    }
}
