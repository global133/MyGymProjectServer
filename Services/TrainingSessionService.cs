using AutoMapper;
using MyGymProject.Server.DTOs.TrainingSession;
using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories.Interfaces;
using MyGymProject.Server.Repositories.InterfacesRepository;
using MyGymProject.Server.Services.Interfaces;
using System.Data;

namespace MyGymProject.Server.Services
{
    public class TrainingSessionService : ITrainingSessionService
    {
        private readonly ITrainingSessionRepository _sessionRepo;
        private readonly IClientRepository _clientRepo;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public TrainingSessionService(
            ITrainingSessionRepository sessionRepo,
            IClientRepository clientRepo,
            ITrainingRepository trainingRepository,
            IMapper mapper)
        {
            _sessionRepo = sessionRepo;
            _clientRepo = clientRepo;
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TrainingSessionReadDto>> GetAllSessionsAsync()
        {
            try
            {
                var sessions = await _sessionRepo.GetAllSessionsAsync();
                return _mapper.Map<IEnumerable<TrainingSessionReadDto>>(sessions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
        public async Task<TrainingSessionReadDto?> GetByIdAsync(int id)
        {
            var session = await _sessionRepo.GetByIdAsync(id);
            return session == null ? null : _mapper.Map<TrainingSessionReadDto>(session);
        }

        public async Task<IEnumerable<TrainingSessionReadDto>> GetByTrainingIdAsync(int trainingId)
        {
            try
            {
                var sessions = await _sessionRepo.GetByTrainingIdAsync(trainingId);
                return _mapper.Map<IEnumerable<TrainingSessionReadDto>>(sessions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<TrainingSessionReadDto> CreateSessionAsync(int trainingId, TrainingSessionCreateDto dto)
        {
            try
            {
                var training = await this._trainingRepository.GetTraining(trainingId);

                if (training == null)
                    return null;
                
                var session = _mapper.Map<TrainingSession>(dto);
                session.Training = training;
                var addedSession = await _sessionRepo.AddAsync(session);
                return _mapper.Map<TrainingSessionReadDto>(addedSession);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateSessionAsync(int id, TrainingSessionCreateDto dto)
        {
            var session = await _sessionRepo.GetByIdAsync(id);
            if (session == null) return false;

            var updatedSession = _mapper.Map<TrainingSession>(dto);
            await _sessionRepo.UpdateAsync(updatedSession);
            return true;
        }

        public async Task<bool> DeleteSessionAsync(int id)
        {
            try
            {
                await _sessionRepo.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddClientToSessionAsync(int sessionId, int clientId)
        {
            var client = await _clientRepo.GetByIdAsync(clientId);
            if (client == null) return false;

            return await _sessionRepo.AddClientToSessionAsync(sessionId, client);
        }

        public async Task<bool> RemoveClientFromSessionAsync(int sessionId, int clientId)
        {
            return await _sessionRepo.RemoveClientFromSessionAsync(sessionId, clientId);
        }

        public async Task<bool> IsClientInSessionAsync(int sessionId, int clientId)
        {
            return await _sessionRepo.IsClientInSessionAsync(sessionId, clientId);
        }

        public async Task<IEnumerable<TrainingSessionReadDto>> GetUpcomingSessionsAsync(int trainingId)
        {
            try
            {
                var fromDate = DateTime.UtcNow;
                var sessions = await _sessionRepo.GetUpcomingSessionsAsync(fromDate, trainingId);
                return _mapper.Map<IEnumerable<TrainingSessionReadDto>>(sessions);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
