using MyGymProject.Server.DTOs.Hall;
using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories.Interfaces;
using MyGymProject.Server.Services.Interfaces;
using AutoMapper;

namespace MyGymProject.Server.Services
{
    public class HallService : IHallService
    {
        private readonly IHallRepository _hallRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<HallService> _logger;

        public HallService(IHallRepository hallRepository, IMapper mapper, ILogger<HallService> logger)
        {
            this._hallRepository = hallRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<IEnumerable<HallDtoResponse>> GetAllHalls()
        {
            try
            {
                var halls = await this._hallRepository.GetAllHalls();
                return this._mapper.Map<IEnumerable<HallDtoResponse>>(halls);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Не удалось получить данные {Methodname}", nameof(GetAllHalls));
                return Enumerable.Empty<HallDtoResponse>();
            }
        }

        public async Task<HallDtoResponse?> GetHall(int id)
        {
            try
            {
                var hall = await this._hallRepository.GetHall(id);
                return hall != null ? this._mapper.Map<HallDtoResponse>(hall) : null;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error getting hall with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<HallDtoResponse> CreateHall(HallCreateDto dto)
        {
            try
            {
                var hall = this._mapper.Map<Hall>(dto);
                var created = await this._hallRepository.AddHall(hall);
                return this._mapper.Map<HallDtoResponse>(created);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error creating hall: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateHall(int id, HallCreateDto dto)
        {
            try
            {
                var existing = await this._hallRepository.GetHall(id);
                if (existing == null)
                    throw new KeyNotFoundException($"Hall with ID {id} not found.");

                // Обновим значения вручную (или через маппер)
                _mapper.Map(dto, existing);
                return await this._hallRepository.UpdateHall(id, existing);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error updating hall with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteHall(int id)
        {
            try
            {
                var existing = await this._hallRepository.GetHall(id);
                if (existing != null)
                {
                    return await this._hallRepository.DeleteHall(id);
                }
                return false;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error deleting hall with ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
