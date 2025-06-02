using MyGymProject.Server.Data;
using MyGymProject.Server.DTOs.Admin;
using MyGymProject.Server.DTOs.Client;
using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection.Metadata.Ecma335;
using MyGymProject.Server.Services.Interfaces;
using MyGymProject.Server.DTOs.Hall;
using MyGymProject.Server.DTOs.Trainer;
using MyGymProject.Server.DTOs.Training;
using System.Collections;
using Microsoft.AspNetCore.Identity;

namespace MyGymProject.Server.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IClientService _clientService;
        private readonly ITrainerService _trainerService;
        private readonly IHallService _hallService;
        private readonly ITrainingService _trainingService;
        private readonly IPasswordHasher<object> _passwordHasher;

        private readonly IMapper _mapper;
        private readonly ILogger<AdminService> _logger;

        public AdminService(
            IAdminRepository adminRepository,
            ILogger<AdminService> logger,
            IMapper mapper,
            IClientService clientService,
            ITrainerService trainerService,
            IHallService hallService,
            ITrainingService trainingService
            )
        {
            this._adminRepository = adminRepository;
            this._logger = logger;
            this._mapper = mapper;
            this._clientService = clientService;
            this._trainerService = trainerService;
            this._hallService = hallService;
            this._trainingService = trainingService;
            this._passwordHasher = new PasswordHasher<object>();
        }

        #region Admin

        public async Task<AdminReadDto?> GetAdminByloginAsync(string login)
        {
            try
            {
                var admin = await this._adminRepository.GetAdminByLoginAsync(login);
                return this._mapper.Map<AdminReadDto>(admin);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, "Не удалось получить админа по логину");
                return null;
            }
        }

        public async Task<AdminReadDto?> CreateAdminAsync(AdminCreateDto dto)
        {
            try
            {
                var existing = this._adminRepository.GetAdminByLoginAsync(dto.Login);

                if (existing != null)
                {
                    this._logger.LogInformation("Админ с таким логином {login} уже существует", dto.Login);
                    return null;
                }

                var admin = this._mapper.Map<Admin>(dto);
                admin.Password = this._passwordHasher.HashPassword(null, dto.Password);
                await this._adminRepository.CreateAdminAsync(admin);

                return this._mapper.Map<AdminReadDto>(admin);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Не получилось добавить админа {login}", dto.Login);
                return null;
            }
        }

        public async Task<AdminReadDto?> GetAdminByIdAsync(int id)
        {
            try
            {
                var admin = await _adminRepository.GetAdminByIdAsync(id);
                return admin == null ? null : _mapper.Map<AdminReadDto>(admin);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении администратора по ID {Id}", id);
                return null;
            }
        }

        public async Task<IEnumerable<AdminReadDto>> GetAllAdminsAsync()
        {
            try
            {
                var admins = await _adminRepository.GetAllAdminsAsync();
                return _mapper.Map<IEnumerable<AdminReadDto>>(admins);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех администраторов");
                return Enumerable.Empty<AdminReadDto>();
            }
        }

        public async Task<bool> UpdateAdminAsync(int id, AdminCreateDto dto)
        {
            try
            {
                var existing = await this._adminRepository.GetAdminByIdAsync(id);
                if (existing == null)
                {
                    this._logger.LogWarning("Админ с ID {Id} не найден для обновления", id);
                    return false;
                }

                this._mapper.Map(dto, existing); 
                return await this._adminRepository.UpdateAdminAsync(existing);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при обновлении администратора ID {Id}", id);
                return false;
            }
        }

        public async Task<bool> DeleteAdminAsync(int id)
        {
            try
            {
                var success = await this._adminRepository.DeleteAdminAsync(id);
                if (!success)
                    this._logger.LogWarning("Не удалось удалить администратора с ID {Id} — не найден", id);

                return success;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при удалении администратора ID {Id}", id);
                return false;
            }
        }
        #endregion


        #region Client
        public async Task<IEnumerable<ClientReadDto>> GetAllUSersAsync()
        {
            try
            {
                var clients = await this._clientService.GetAllAsync();
                return clients;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при выполнении операции в {MethodName}", nameof(GetAllUSersAsync));
                return Enumerable.Empty<ClientReadDto>();
            }
        }

        public async Task<ClientReadDto?> GetClientByIdAsync(int id)
        {
            try
            {
                return await this._clientService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Не получилось получить данные {MethodName}", nameof(GetClientByIdAsync));
                return null;
            }
        }
        public async Task<bool> AddClientAsync(ClientCreateDto dto)
        {
            try
            { 
                await this._clientService.AddAsync(dto);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Не удалось добавить клиента {MethodName}", nameof(AddClientAsync));
                return false;
            }
        }

        public async Task<bool> UpdateClientAsync(int id, ClientCreateDto client)
        {
            try
            {
                await this._clientService.UpdateAsync(id, client);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "не удалось обновить клиента {Method Name}", nameof(UpdateClientAsync));
                return false;
            }
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            try
            {
                await this._clientService.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "не удалось удалить клиента {Method Name}", nameof(DeleteClientAsync));
                return false;
            }
        }
        #endregion 

        #region Trainers

    public async Task<IEnumerable<TrainerReadDto>> GetAllTrainersWithTrainingsAsync()
    {
            try
            {
                return await this._trainerService.GetAllAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "не удалось получить расписание тренера {Method Name}", nameof(GetAllTrainersWithTrainingsAsync));
                return Enumerable.Empty<TrainerReadDto>();
            }
    }

        public async Task<TrainerReadDto?> GetTrainerByIdAsync(int trainerId)
        {
            try
            {
                return await this._trainerService.GetByIdAsync(trainerId);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при получении тренера по ID: {TrainerId}", trainerId);
                return null;
            }
        }

        public async Task<TrainerReadDto?> AddTrainerAsync(TrainerCreateDto dto)
        {
            try
            {
                return await this._trainerService.AddAsync(dto);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при добавлении тренера");
                return null;
            }
        }

        public async Task<bool> UpdateTrainerAsync(int trainerId, TrainerCreateDto dto)
        {
            try
            {
                return await this._trainerService.UpdateAsync(trainerId, dto);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при обновлении тренера: {TrainerId}", trainerId);
                return false;
            }
        }

        public async Task<bool> DeleteTrainerAsync(int trainerId)
        {
            try
            {
                return await this._trainerService.DeleteAsync(trainerId);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при удалении тренера: {TrainerId}", trainerId);
                return false;
            }
        }

        #endregion

        #region Trainings

        public async Task<IEnumerable<TrainingResponseDTO>> GetAllTrainingsAsync()
        {
            try
            {
                return await this._trainingService.GetAllTrainingsWithDetailsAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при получении всех тренировок");
                return Enumerable.Empty<TrainingResponseDTO>();
            }
        }

        public async Task<TrainingResponseDTO?> GetTrainingByIdAsync(int trainingId)
        {
            try
            {
                return await this._trainingService.GetTraining(trainingId);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при получении тренировки по ID: {TrainingId}", trainingId);
                return null;
            }
        }

        public async Task<TrainingResponseDTO?> AddTrainingAsync(TrainingCreateDto dto)
        {
            try
            {
                return await this._trainingService.CreateTraining(dto);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при создании тренировки");
                return null;
            }
        }

        public async Task<bool> UpdateTrainingAsync(int trainingId, TrainingCreateDto dto)
        {
            try
            {
                return await this._trainingService.UpdateTraining(trainingId, dto);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при обновлении тренировки: {TrainingId}", trainingId);
                return false;
            }
        }

        public async Task<bool> DeleteTrainingAsync(int trainingId)
        {
            try
            {
                return await this._trainingService.DeleteTraining(trainingId);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при удалении тренировки: {TrainingId}", trainingId);
                return false;
            }
        }

        #endregion

        #region Halls

        public async Task<IEnumerable<HallDtoResponse>> GetAllHallsAsync()
        {
            try
            {
                return await this._hallService.GetAllHalls();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Не удалось получить данные {Methodname}", nameof(GetAllHallsAsync));
                return Enumerable.Empty<HallDtoResponse>();
            }
        }
        public async Task<HallDtoResponse?> GetHallByIdAsync(int hallId)
        {
            try
            {
                return await this._hallService.GetHall(hallId);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при получении зала по ID: {HallId}", hallId);
                return null;
            }
        }

        public async Task<HallDtoResponse?> AddHallAsync(HallCreateDto dto)
        {
            try
            {
                return await this._hallService.CreateHall(dto);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при добавлении зала");
                return null;
            }
        }

        public async Task<bool> UpdateHallAsync(int hallId, HallCreateDto dto)
        {
            try
            {
                return await this._hallService.UpdateHall(hallId, dto);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при обновлении зала: {HallId}", hallId);
                return false;
            }
        }

        public async Task<bool> DeleteHallAsync(int hallId)
        {
            try
            {
                return await this._hallService.DeleteHall(hallId);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Ошибка при удалении зала: {HallId}", hallId);
                return false;
            }
        }

        #endregion
    }
}

