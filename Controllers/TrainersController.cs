using Microsoft.AspNetCore.Mvc;
using MyGymProject.Server.DTOs.Trainer;
using MyGymProject.Server.Models;
using MyGymProject.Server.Services.Interfaces;


namespace MyGymProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainersController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        // GET: api/Trainers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerReadDto>>> GetAll()
        {
            var trainers = await _trainerService.GetAllAsync();
            return Ok(trainers);
        }

        // GET: api/Trainers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerReadDto>> GetById(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null)
                return NotFound();

            return Ok(trainer);
        }

        // GET: api/Trainers/login/johndoe
        [HttpGet("login/{login}")]
        public async Task<ActionResult<Trainer>> GetByLogin(string login)
        {
            var trainer = await _trainerService.GetByLoginAsync(login);
            if (trainer == null)
                return NotFound();

            return Ok(trainer);
        }

        // POST: api/Trainers
        [HttpPost]
        public async Task<ActionResult<TrainerReadDto>> Add([FromBody] TrainerCreateDto dto)
        {
            var createdTrainer = await _trainerService.AddAsync(dto);
            if (createdTrainer == null)
                return Conflict(new { message = $"Trainer with login '{dto.Login}' already exists." });

            return Ok(createdTrainer);
        }

        // PUT: api/Trainers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrainerCreateDto dto)
        {
            var success = await _trainerService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Trainers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _trainerService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
