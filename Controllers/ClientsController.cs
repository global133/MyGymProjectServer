using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGymProject.Server.DTOs.Client;
using MyGymProject.Server.Services.Interfaces;


namespace MyGymProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            this._clientService = clientService;
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
            var client = await this._clientService.GetByIdAsync(id);
            if (client == null)
                return NotFound();

            return Ok(client);
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
        public async Task<IActionResult> Update(int id, ClientUpdateDto dto)
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
    }
}

