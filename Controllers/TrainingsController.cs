
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGymProject.Server.DTOs.Training;
using MyGymProject.Server.Services.Interfaces;
using System.Security.Claims;

namespace MyGymProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingsController : ControllerBase
    {
        private readonly ITrainingService _trainingService;
        private readonly ILogger<TrainingsController> _logger;

        public TrainingsController(ITrainingService trainingService, ILogger<TrainingsController> logger)
        {
            _trainingService = trainingService;
            _logger = logger;
        }

        // GET api/trainings/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingResponseDTO>> GetTraining(int id)
        {
            var training = await _trainingService.GetTraining(id);
            if (training == null)
                return NotFound();

            return Ok(training);
        }

        // POST api/trainings
        [HttpPost]
        public async Task<ActionResult<TrainingResponseDTO>> CreateTraining([FromBody] TrainingCreateDto dto)
        {
            try
            {
                var createdTraining = await _trainingService.CreateTraining(dto);
                return CreatedAtAction(nameof(GetTraining), new { id = createdTraining.Id }, createdTraining);
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Validation error when creating training.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when creating training.");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT api/trainings/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTraining(int id, [FromBody] TrainingCreateDto updatedDto)
        {
            try
            {
                var updated = await _trainingService.UpdateTraining(id, updatedDto);
                if (!updated)
                    return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Trainer or Hall not found when updating training with ID {id}.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating training with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE api/trainings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraining(int id)
        {
            try
            {
                var deleted = await _trainingService.DeleteTraining(id);
                if (!deleted)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting training with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/trainings/trainer/{trainerId}
        [HttpGet("trainer/{trainerId}")]
        public async Task<ActionResult<IEnumerable<TrainingResponseDTO>>> GetScheduleForTrainer(int trainerId)
        {
            var schedule = await _trainingService.GetScheduleForTrainerAsync(trainerId);
            return Ok(schedule);
        }

        // POST api/trainings/{trainingId}/clients/{clientId}
        [HttpPost("{trainingId}/clients/{clientId}")]
        public async Task<IActionResult> AddClientToTraining(int trainingId, int clientId)
        {
            var success = await _trainingService.AddClientToTrainingAsync(trainingId, clientId);
            if (!success)
                return BadRequest("Cannot add client to training.");

            return NoContent();
        }

        // DELETE api/trainings/{trainingId}/clients/{clientId}
        [HttpDelete("{trainingId}/clients/{clientId}")]
        public async Task<IActionResult> RemoveClientFromTraining(int trainingId, int clientId)
        {
            var success = await _trainingService.RemoveClientFromTrainingAsync(trainingId, clientId);
            if (!success)
                return BadRequest("Cannot remove client from training.");

            return NoContent();
        }

        // GET api/trainings/{trainingId}/clients
        [HttpGet("{trainingId}/clients")]
        public async Task<ActionResult<List<string>>> GetClientsForTraining(int trainingId)
        {
            var clients = await _trainingService.GetClientsForTrainingAsync(trainingId);
            return Ok(clients);
        }

        // GET api/trainings/details
        [HttpGet("details")]
        public async Task<ActionResult<IEnumerable<TrainingResponseDTO>>> GetAllTrainingsWithDetails()
        {
            var trainings = await _trainingService.GetAllTrainingsWithDetailsAsync();
            return Ok(trainings);
        }

        [HttpGet("my-schedule")]
        public async Task<ActionResult<IEnumerable<TrainingResponseDTO>>> GetScheduleForCurrentClient()
        {
            try
            {
                var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (clientIdClaim == null || !int.TryParse(clientIdClaim.Value, out int clientId))
                {
                    return Unauthorized("Не удалось определить ID клиента.");
                }

                var trainings = await _trainingService.GetTrainingsByClient(clientId);
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении расписания клиента.");
                return StatusCode(500, "Внутренняя ошибка сервера.");
            }
        }

        [HttpGet("{trainingName}/trainer{trainerId}")]

        public async Task<ActionResult<IEnumerable<TrainingResponseDTO>>> GetTrainingsByTrainerAndNameAsync(int trainerId, string trainingName)
        {
            var trainings = await this._trainingService.GetTrainingsByTrainerAndNameAsync(trainerId, trainingName);
            return Ok(trainings);
        }
    }
}
