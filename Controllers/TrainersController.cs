using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MyGymProject.Server.DTOs.Trainer;
using MyGymProject.Server.Models;
using MyGymProject.Server.Services.Interfaces;
using System.Text.Json;

namespace MyGymProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly IDistributedCache _cache;

        public TrainersController(ITrainerService trainerService, IDistributedCache cache)
        {
            _trainerService = trainerService;
            _cache = cache;
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
            var cacheKey = $"trainer_{id}";
            try
            {
                var chachedData = await _cache.GetStringAsync(cacheKey);

                if (!string.IsNullOrEmpty(chachedData))
                {
                    var trainer = JsonSerializer.Deserialize<TrainerReadDto>(chachedData);
                    return Ok(trainer);
                }
                var trainerFromDb = await _trainerService.GetByIdAsync(id);
                if (trainerFromDb == null)
                    return NotFound();

                var jsonData = JsonSerializer.Serialize(trainerFromDb);

                await _cache.SetStringAsync(cacheKey, jsonData, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
                return Ok(trainerFromDb);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return NotFound();
            }
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
