using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyGymProject.Server.Controllers
{
    using global::MyGymProject.Server.DTOs.TrainingSession;
    using global::MyGymProject.Server.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    namespace MyGymProject.Server.Controllers
    {
        [ApiController]
        [Route("api/trainings/{trainingId}/sessions")]
        public class TrainingSessionsController : ControllerBase
        {
            private readonly ITrainingSessionService _sessionService;

            public TrainingSessionsController(ITrainingSessionService sessionService)
            {
                _sessionService = sessionService;
            }

            [HttpGet("all")]
            public async Task<ActionResult<IEnumerable<TrainingSessionCreateDto>>> GetAllSessions()
            {
                var sessions = await _sessionService.GetAllSessionsAsync();
                return Ok(sessions);
            }
            /// <summary>
            /// Получить сессию по ID
            /// </summary>
            [HttpGet("{id}")]
            public async Task<ActionResult<TrainingSessionCreateDto>> GetById(int trainingId, int id)
            {
                var session = await _sessionService.GetByIdAsync(id);
                return session == null ? NotFound() : Ok(session);
            }

            /// <summary>
            /// Получить все сессии для тренировки
            /// </summary>
            [HttpGet]
            public async Task<ActionResult<IEnumerable<TrainingSessionCreateDto>>> GetByTrainingIdAsync(int trainingId)
            {
                var sessions = await _sessionService.GetByTrainingIdAsync(trainingId);
                return Ok(sessions);
            }

            /// <summary>
            /// Создать новую сессию
            /// </summary>
            [HttpPost]
            public async Task<ActionResult<TrainingSessionCreateDto>> Create(int trainingId, TrainingSessionCreateDto dto)
            {
                var createdSession = await _sessionService.CreateSessionAsync(trainingId, dto);
                if (createdSession == null) 
                    return BadRequest("Не удалось создать сессию");
                 
                return Ok(createdSession);
            }

            /// <summary>
            /// Обновить сессию
            /// </summary>
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int trainingId, int id, TrainingSessionCreateDto dto)
            {
                var success = await _sessionService.UpdateSessionAsync(id, dto);
                return success ? NoContent() : NotFound();
            }

            /// <summary>
            /// Удалить сессию
            /// </summary>
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int trainingId, int id)
            {
                var success = await _sessionService.DeleteSessionAsync(id);
                return success ? NoContent() : NotFound();
            }

            /// <summary>
            /// Добавить клиента на сессию
            /// </summary>
            [HttpPost("{sessionId}/clients/{clientId}")]
            public async Task<IActionResult> AddClient(int sessionId, int clientId)
            {
                var success = await _sessionService.AddClientToSessionAsync(sessionId, clientId);
                return success ? Ok() : BadRequest("Не удалось добавить клиента");
            }

            /// <summary>
            /// Удалить клиента с сессии
            /// </summary>
            [HttpDelete("{sessionId}/clients/{clientId}")]
            public async Task<IActionResult> RemoveClient(int sessionId, int clientId)
            {
                var success = await _sessionService.RemoveClientFromSessionAsync(sessionId, clientId);
                return success ? NoContent() : NotFound();
            }

            /// <summary>
            /// Проверить доступность сессии
            /// </summary>
            [HttpGet("{sessionId}/availability")]
            public async Task<ActionResult<bool>> CheckAvailability(int sessionId)
            {
                var isAvailable = await _sessionService.IsSessionAvailableAsync(sessionId);
                return Ok(isAvailable);
            }

            /// <summary>
            /// Получить количество свободных мест
            /// </summary>
            [HttpGet("{sessionId}/slots")]
            public async Task<ActionResult<int>> GetAvailableSlots(int sessionId)
            {
                var slots = await _sessionService.GetAvailableSlotsAsync(sessionId);
                return Ok(slots);
            }

            /// <summary>
            /// Получить предстоящие сессии
            /// </summary>
            [HttpGet("upcoming")]
            public async Task<ActionResult<IEnumerable<TrainingSessionCreateDto>>> GetUpcomingSessions(int trainingId)
            {
                var sessions = await _sessionService.GetUpcomingSessionsAsync(trainingId);
                return Ok(sessions);
            }

            [HttpGet("")]
        }
    }
}
