using MyGymProject.Server.DTOs.Trainer;
using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories;
using MyGymProject.Server.Repositories.Interfaces;
using MyGymProject.Server.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace MyGymProject.Server.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<TrainerService> _logger;
        private readonly IPasswordHasher<object> _passwordHasher;

        public TrainerService(ITrainerRepository repository, IMapper mapper, ILogger<TrainerService> logger)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._logger = logger;
            this._passwordHasher = new PasswordHasher<object>();
        }

        public async Task<IEnumerable<TrainerReadDto>> GetAllAsync()
        {
            try
            {
                var trainers = await this._repository.GetAllAsync();
                return this._mapper.Map<IEnumerable<TrainerReadDto>>(trainers);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Ошибка при получении всех тренеров: {ex.Message}");
                return Enumerable.Empty<TrainerReadDto>();
            }
        }

        public async Task<TrainerReadDto?> GetByIdAsync(int id)
        {
            try
            {
                var trainer = await this._repository.GetByIdAsync(id);
                return trainer == null ? null : _mapper.Map<TrainerReadDto>(trainer);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Ошибка при получении тренера по ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<Trainer?> GetByLoginAsync(string login)
        {
            try
            {
                return await this._repository.GetByLoginAsync(login);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Ошибка при получении тренера по логину {login}: {ex.Message}");
                return null;
            }
        }

        public async Task<TrainerReadDto?> AddAsync(TrainerCreateDto dto)
        {
            try
            {
                var exists = await this._repository.GetByLoginAsync(dto.Login);
                if (exists != null)
                {
                    this._logger.LogError($"Тренер с логином {dto.Login} уже существует.");
                    return null;
                }

                var trainer = this._mapper.Map<Trainer>(dto);
                trainer.Password = this._passwordHasher.HashPassword(null, dto.Password);
                var addedTrainer = await this._repository.AddAsync(trainer);

                return this._mapper.Map<TrainerReadDto>(addedTrainer);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Ошибка при добавлении тренера: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateAsync(int id, TrainerCreateDto dto)
        {
            try
            {
                var existing = await this._repository.GetByIdAsync(id);
                if (existing == null)
                    return false;

                this._mapper.Map(dto, existing); // обновляет поля объекта
                return await this._repository.UpdateAsync(existing);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Ошибка при обновлении тренера ID {id}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await this._repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Ошибка при удалении тренера ID {id}: {ex.Message}");
                return false;
            }
        }
    }
}