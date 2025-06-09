using MyGymProject.Server.DTOs.Client;
using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories.Interfaces;
using MyGymProject.Server.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace MyGymProject.Server.Services
{
    public class ClientService: IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientService> _logger;
        private readonly IPasswordHasher<object> _passwordHasher;

        public ClientService(IClientRepository clientRepository, IMapper mapper, ILogger<ClientService> logger)
        {
            this._clientRepository = clientRepository;
            this._mapper = mapper;
            this._logger = logger;
            this._passwordHasher = new PasswordHasher<object>();
        }

        public async Task<IEnumerable<ClientReadDto>> GetAllAsync()
        {
            try
            {
                var clients = await this._clientRepository.GetAllAsync();
                return this._mapper.Map<IEnumerable<ClientReadDto>>(clients);
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex.Message, "Ошибка при получении всех клиентов");
                return Enumerable.Empty<ClientReadDto>();
            }
        }

        public async Task<ClientReadDto?> GetByIdAsync(int id)
        {
            try
            {
                var client = await this._clientRepository.GetByIdAsync(id);
                return client == null ? null : _mapper.Map<ClientReadDto>(client);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, $"Ошибка при получении клиента по ID: {id}");
                return null;
            }
        }

        public async Task<Client?> GetByLoginAsync(string login)
        {
            try
            {
                return await this._clientRepository.GetByLoginAsync(login);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, $"Ошибка при получении клиента по логину: {login}");
                return null;
            }
        }

        public async Task<bool> AddAsync(ClientCreateDto createDto)
        {
            try
            {
                var existing = await this._clientRepository.GetByLoginAsync(createDto.Login);
                if (existing != null)
                {
                   this._logger.LogError("Попытка регистрации с существующим логином: {Login}", createDto.Login);
                    return false; 
                }

                var client = _mapper.Map<Client>(createDto);
                client.Password = this._passwordHasher.HashPassword(null, client.Password);
                await this._clientRepository.AddAsync(client);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, "Ошибка при добавлении клиента");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(int id, ClientUpdateDto updateDto)
        {
            try
            {
                var client = await this._clientRepository.GetByIdAsync(id);
                if (client == null)
                    return false;

                _mapper.Map(updateDto, client);
                await this._clientRepository.UpdateAsync(client);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, $"Ошибка при обновлении клиента: {id}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var client = await this._clientRepository.GetByIdAsync(id);
                if (client == null)
                    return false;

                await this._clientRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, $"Ошибка при удалении клиента: {id}");
                return false;
            }
        }

        public async Task<bool> AddTrainingAsync(int clientId, Training training)
        {
            try
            {
                await _clientRepository.AddTrainingToClientAsync(clientId, training);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, $"Ошибка при добавлении тренировки клиенту {clientId}");
                return false;
            }
        }
    }
}
