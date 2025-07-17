using AutoMapper;
using MyGymProject.Server.DTOs.TrainingSession;
using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories.Interfaces;
using MyGymProject.Server.Repositories.InterfacesRepository;
using MyGymProject.Server.Services.Interfaces;

public class TrainingSessionService : ITrainingSessionService
{
    private readonly ITrainingSessionRepository _sessionRepo;
    private readonly IClientRepository _clientRepo;
    private readonly ITrainerRepository _trainerRepo;
    private readonly IHallRepository _hallRepo;
    private readonly IMapper _mapper;

    public TrainingSessionService(
        ITrainingSessionRepository sessionRepo,
        IClientRepository clientRepo,
        ITrainerRepository trainerRepo,
        IHallRepository hallRepo,
        IMapper mapper)
    {
        _sessionRepo = sessionRepo;
        _clientRepo = clientRepo;
        _trainerRepo = trainerRepo;
        _hallRepo = hallRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TrainingSessionReadDto>> GetAllSessionsAsync()
    {
        var sessions = await _sessionRepo.GetAllSessionsAsync();
        return _mapper.Map<IEnumerable<TrainingSessionReadDto>>(sessions);
    }

    public async Task<TrainingSessionReadDto?> GetByIdAsync(int id)
    {
        var session = await _sessionRepo.GetTraining(id);
        return session == null ? null : _mapper.Map<TrainingSessionReadDto>(session);
    }

    public async Task<TrainingSessionReadDto?> CreateSessionAsync(TrainingSessionCreateDto dto)
    {
        var trainer = await _trainerRepo.GetByIdAsync(dto.TrainerId);
        var hall = await _hallRepo.GetHall(dto.HallId);

        if (trainer == null || hall == null)
            return null;

        var session = _mapper.Map<TrainingSession>(dto);
        session.Trainer = trainer;
        session.Hall = hall;

        var added = await _sessionRepo.AddTraining(session);
        return _mapper.Map<TrainingSessionReadDto>(added);
    }

    public async Task<bool> UpdateSessionAsync(int id, TrainingSessionCreateDto dto)
    {
        var existing = await _sessionRepo.GetTraining(id);
        if (existing == null)
            return false;

        var trainer = await _trainerRepo.GetByIdAsync(dto.TrainerId);
        var hall = await _hallRepo.GetHall(dto.HallId);
        if (trainer == null || hall == null)
            return false;

        existing.Name = dto.Name;
        existing.IsGroup = dto.IsGroup;
        existing.StartTime = DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc);
        existing.EndTime = DateTime.SpecifyKind(dto.EndTime, DateTimeKind.Utc);
        existing.Trainer = trainer;
        existing.Hall = hall;

        await _sessionRepo.UpdateTraining(existing);
        return true;
    }

    public async Task<bool> DeleteSessionAsync(int id)
    {
        return await _sessionRepo.DeleteTraining(id);
    }

    public async Task<bool> AddClientToSessionAsync(int sessionId, int clientId)
    {
        try
        {
            return await _sessionRepo.AddClientToSessionAsync(sessionId, clientId);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> RemoveClientFromSessionAsync(int sessionId, int clientId)
    {
        return await _sessionRepo.RemoveClientFromSessionAsync(sessionId, clientId);
    }

    public async Task<IEnumerable<TrainingSessionReadDto>> GetUpcomingSessionsAsync(int trainingId)
    {
        var sessions = await _sessionRepo.GetUpcomingSessionsAsync(DateTime.UtcNow, trainingId);
        return _mapper.Map<IEnumerable<TrainingSessionReadDto>>(sessions);
    }

    public async Task<IEnumerable<TrainingSessionReadDto>> GetSessionsByTrainerAsync(int trainerId)
    {
        var sessions = await _sessionRepo.GetSessionsByTrainerIdAsync(trainerId);
        return _mapper.Map<IEnumerable<TrainingSessionReadDto>>(sessions);
    }
}
