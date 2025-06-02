using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MyGymProject.Server.DTOs.Client;
using MyGymProject.Server.DTOs.DTOLogin;
using MyGymProject.Server.Models;
using MyGymProject.Server.Services.Interfaces;

namespace MyGymProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IClientService _clientService;
        private readonly ITrainerService _trainerService;
        private readonly IAdminService _adminService;

        public AuthController(
            IAuthService authService,
            IClientService clientService,
            ITrainerService trainerService,
            IAdminService adminService)
        {
            _authService = authService;
            _clientService = clientService;
            _trainerService = trainerService;
            _adminService = adminService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var result = await _authService.LoginAsync(request.Login, request.Password);
            if (!result.Success)
                return Unauthorized(new { result.Message });

            return Ok(new
            {
                token = result.Token,
                role = result.Role,
                message = result.Message
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterClient([FromBody] ClientCreateDto dto)
        {
            var success = await this._clientService.AddAsync(dto);

            if (success == true)
                return Ok(new { message = "Клиент успешно зарегистрирован" });

            return BadRequest(new { message = "Ошибка при регистрации. Логин уже существует или произошла внутренняя ошибка." });
        }
    }
}
