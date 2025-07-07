using MyGymProject.Server.DTOs.Training;
using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories.Interfaces;
using MyGymProject.Server.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace MyGymProject.Server.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IHallRepository _hallRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<TrainingService> _logger;

        public TrainingService(
        ITrainingRepository trainingRepository,
        ITrainerRepository trainerRepository,
        IHallRepository hallRepository,
        IMapper mapper,
        ILogger<TrainingService> logger)
        {
            this._trainingRepository = trainingRepository;
            this._trainerRepository = trainerRepository;
            this._hallRepository = hallRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<TrainingResponseDTO?> GetTraining(int id)
        {
            try
            {
                var training = await this._trainingRepository.GetTraining(id);

                if (training == null)
                    return null;

                return this._mapper.Map<TrainingResponseDTO>(training);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error retrieving training with ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<TrainingResponseDTO> CreateTraining(TrainingCreateDto dto)
        {
            try
            {
                var training = this._mapper.Map<Training>(dto);

                training.Trainer = await this._trainerRepository.GetByIdAsync(dto.TrainerId)
                                ?? throw new ApplicationException("Trainer not found.");

                training.Hall = await this._hallRepository.GetHall(dto.HallId)
                                 ?? throw new ApplicationException("Hall not found.");

                var added = await this._trainingRepository.AddTraining(training);
                return this._mapper.Map<TrainingResponseDTO>(added);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateTraining(int id, TrainingCreateDto updatedDto)
        {
            try
            {
                var trainer = await this._trainerRepository.GetByIdAsync(updatedDto.TrainerId);
                var hall = await this._hallRepository.GetHall(updatedDto.HallId);

                if (trainer == null || hall == null)
                    throw new InvalidOperationException("Trainer or Hall not found.");

                // Маппинг DTO в Training
                var updatedTraining = this._mapper.Map<Training>(updatedDto);
                updatedTraining.Trainer = trainer;
                updatedTraining.Hall = hall;

                return await this._trainingRepository.UpdateTraining(id, updatedTraining);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error updating training with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteTraining(int id)
        {
            try
            {
                var result = await this._trainingRepository.DeleteTraining(id);
                if (!result)
                    throw new InvalidOperationException($"Training with ID {id} not found.");

                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error deleting training with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<TrainingResponseDTO>> GetScheduleForTrainerAsync(int trainerId)
        {
            try
            {
                var trainings = await this._trainingRepository.GetScheduleForTrainerAsync(trainerId);
                return this._mapper.Map<IEnumerable<TrainingResponseDTO>>(trainings);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Ошибка при получении расписания тренера: {ex.Message}");
                return Enumerable.Empty<TrainingResponseDTO>();
            }
        }

        public async Task<bool> AddClientToTrainingAsync(int trainingId, int clientId)
        {
            try
            {
                return await this._trainingRepository.AddClientToTrainingAsync(trainingId, clientId);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Ошибка при записи клиента: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveClientFromTrainingAsync(int trainingId, int clientId)
        {
            try
            {
                return await this._trainingRepository.RemoveClientFromTrainingAsync(trainingId, clientId);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"[Service] Ошибка при удалении клиента {clientId}: {ex.Message}");
                return false;
            }
        }

        public async Task<List<string>> GetClientsForTrainingAsync(int trainingId)
        {
            try
            {
                return await this._trainingRepository.GetClientsForTrainingAsync(trainingId);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Ошибка при получении клиентов для тренировки {trainingId}: {ex.Message}");
                return new List<string>();
            }
        }

        public async Task<IEnumerable<TrainingResponseDTO>> GetAllTrainingsWithDetailsAsync()
        {
            try
            {
                var trainings = await this._trainingRepository.GetAllTrainingsWithClientsAsync();
                return _mapper.Map<IEnumerable<TrainingResponseDTO>>(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех тренировок с клиентами, тренерами и залами.");
                return Enumerable.Empty<TrainingResponseDTO>();
            }
        }
        
        public async Task<IEnumerable<TrainingResponseDTO>> GetTrainingsByClient(int clientId)
        {
            try
            {
                var trainings = await this._trainingRepository.GetTrainingsByClient(clientId);
                return this._mapper.Map<IEnumerable<TrainingResponseDTO>>(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении тренировок по айди клиента.");
                return Enumerable.Empty<TrainingResponseDTO>();
            }
        }

        public async Task<IEnumerable<TrainingResponseDTO>> GetTrainingsByTrainerAndNameAsync(int trainerId, string trainingName)
        {
            try
            {
                var trainings = await this._trainingRepository.GetTrainingsByTrainerAndNameAsync(trainerId, trainingName);
                return this._mapper.Map<IEnumerable<TrainingResponseDTO>>(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении тренировок по айди тренера и имени тренировки.");
                return Enumerable.Empty<TrainingResponseDTO>();
            }
        }
    }
}
