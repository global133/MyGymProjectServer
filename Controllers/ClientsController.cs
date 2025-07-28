using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MyGymProject.Server.DTOs.Client;
using MyGymProject.Server.Models;
using MyGymProject.Server.Services.Interfaces;
using System.Text.Json;


namespace MyGymProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Client")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IDistributedCache _cache;

        public ClientsController(IClientService clientService, IDistributedCache cache)
        {
            this._clientService = clientService;
            this._cache = cache;
        }

        // GET: api/clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientReadDto>>> GetAll()
        {
            var clients = await this._clientService.GetAllAsync();
            return Ok(clients);
        }

        // GET: api/clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientReadDto>> GetById(int id)
        {
            var cacheKey = $"client_{id}";
            try
            {
                var cachedData = await this._cache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    var data = JsonSerializer.Deserialize<ClientReadDto>(cachedData);
                    return Ok(data);
                }

                var clientFromDb = await this._clientService.GetByIdAsync(id);

                if (clientFromDb == null)
                    return NotFound();

                var jsonData = JsonSerializer.Serialize(clientFromDb);
                await this._cache.SetStringAsync(cacheKey, jsonData, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)});
                return Ok(clientFromDb);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return NotFound();
            }
        }

        [HttpGet("bylogin/{login}")]

        public async Task<ActionResult<ClientReadDto>> GetByLogin(string login)
        {
            var client = await this._clientService.GetByLoginAsync(login);
            if (client == null)
                return NotFound();
            return Ok(client);
        }
        // POST: api/clients
        [HttpPost]
        public async Task<ActionResult> Create(ClientCreateDto dto)
        {
            var success = await this._clientService.AddAsync(dto);
            if (!success)
                return Conflict("Клиент с таким логином уже существует.");

            return CreatedAtAction(nameof(GetById), new { id = dto.Login }, dto);
        }

        // PUT: api/clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientUpdateDto dto)
        {
            var success = await this._clientService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await this._clientService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpPost("{clientId}/addtotraining{trainingId}")]

        public async Task<ActionResult> AddClientToTrainingSession(int clientId, int trainingId)
        {
            try
            {
                var result = await _clientService.AddTrainingAsync(clientId, trainingId);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("schedule{id}")]

        public async Task<ActionResult> GetWorkoutByCLientId(int id)
        {
            try
            {
                var result = await _clientService.GetWorkoutByClientId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Произошла ошибка"
                });
            }
        }
    }
}

