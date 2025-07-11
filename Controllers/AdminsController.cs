using Microsoft.AspNetCore.Mvc;
using MyGymProject.Server.DTOs.Client;
using MyGymProject.Server.DTOs.Hall;
using MyGymProject.Server.DTOs.Trainer;
using MyGymProject.Server.DTOs.TrainingSession;
using MyGymProject.Server.Services;
using MyGymProject.Server.Services.Interfaces;



namespace MyGymProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            this._adminService = adminService;
        }

        #region Clients

        [HttpGet("clients")]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await this._adminService.GetAllUSersAsync();
            return Ok(clients);
        }

        [HttpGet("clients/{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await this._adminService.GetClientByIdAsync(id);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost("clients")]
        public async Task<IActionResult> AddClient([FromBody] ClientCreateDto dto)
        {
            var success = await this._adminService.AddClientAsync(dto);
            if (success == null)
                return StatusCode(500, "Ошибка при добавлении клиента");

            return Ok();
        }

        #endregion

        #region Trainers

        [HttpGet("trainers")]
        public async Task<IActionResult> GetAllTrainers()
        {
            var trainers = await this._adminService.GetAllTrainersWithTrainingsAsync();
            return Ok(trainers);
        }

        [HttpGet("trainers/{id}")]
        public async Task<IActionResult> GetTrainerById(int id)
        {
            var trainer = await this._adminService.GetTrainerByIdAsync(id);
            if (trainer == null)
                return NotFound();

            return Ok(trainer);
        }

        [HttpPost("trainers")]
        public async Task<IActionResult> AddTrainer([FromBody] TrainerCreateDto dto)
        {
            var trainer = await this._adminService.AddTrainerAsync(dto);

            if (trainer == null)
                return StatusCode(500, "Не удалось добавить тренера");

            return Ok(trainer);
        }

        [HttpPut("trainers/{id}")]
        public async Task<IActionResult> UpdateTrainer(int id, [FromBody] TrainerCreateDto dto)
        {
            var success = await this._adminService.UpdateTrainerAsync(id, dto);
            if (!success)
                return StatusCode(500, "не удалось обновить тренера");

            return NoContent();
        }

        [HttpDelete("trainers/{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var success = await this._adminService.DeleteTrainerAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        #endregion

        #region Trainings

        [HttpGet("trainings")]
        public async Task<IActionResult> GetAllTrainings()
        {
            var trainings = await this._adminService.GetAllTrainingsAsync();
            return Ok(trainings);
        }

        [HttpGet("trainings/{id}")]
        public async Task<IActionResult> GetTrainingById(int id)
        {
            var training = await this._adminService.GetTrainingByIdAsync(id);
            if (training == null)
                return NotFound();

            return Ok(training);
        }

        [HttpPost("trainings")]
        public async Task<IActionResult> AddTraining([FromBody] TrainingSessionCreateDto dto)
        {
            var training = await this._adminService.AddTrainingAsync(dto);
            return Ok(training);
        }

        [HttpPut("trainings/{id}")]
        public async Task<IActionResult> UpdateTraining(int id, [FromBody] TrainingSessionCreateDto dto)
        {
            var success = await this._adminService.UpdateTrainingAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("trainings/{id}")]
        public async Task<IActionResult> DeleteTraining(int id)
        {
            var success = await this._adminService.DeleteTrainingAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        #endregion

        #region Halls
        [HttpGet("halls")]

        public async Task<IActionResult> GetAllHalls()
        {
            var halls = await this._adminService.GetAllHallsAsync();

            if (!halls.Any())
                return NoContent(); 

            return Ok(halls);
        }


        [HttpGet("halls/{id}")]
        public async Task<IActionResult> GetHallById(int id)
        {
            var hall = await this._adminService.GetHallByIdAsync(id);
            if (hall == null)
                return NotFound();

            return Ok(hall);
        }

        [HttpPost("halls")]
        public async Task<IActionResult> AddHall([FromBody] HallCreateDto dto)
        {
            var hall = await this._adminService.AddHallAsync(dto);
            return Ok(hall);
        }

        [HttpPut("halls/{id}")]
        public async Task<IActionResult> UpdateHall(int id, [FromBody] HallCreateDto dto)
        {
            var success = await this._adminService.UpdateHallAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("halls/{id}")]
        public async Task<IActionResult> DeleteHall(int id)
        {
            var success = await this._adminService.DeleteHallAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        #endregion
    }
}
