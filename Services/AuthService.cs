using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyGymProject.Server.Repositories.Interfaces;
using MyGymProject.Server.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyGymProject.Server.Services
{
    public class AuthService: IAuthService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<object> _passwordHasher;

        public AuthService(
            IClientRepository clientService,
            ITrainerRepository trainerService,
            IAdminRepository adminService,
            IConfiguration config,
            IPasswordHasher<object> passwordHasher)
        {
            this._clientRepository = clientService;
            this._trainerRepository = trainerService;
            this._adminRepository = adminService;
            this._config = config;
            this._passwordHasher = passwordHasher;
        }

        public async Task<(bool Success, string? Token, string? Role, string Message)> LoginAsync(string login, string password)
        {
            var client = await this._clientRepository.GetByLoginAsync(login);
            if (client != null && VerifyPassword(client.Password, password))
                return (true, GenerateToken(login, "Client"), "Client", "Успешный вход");

            var trainer = await this._trainerRepository.GetByLoginAsync(login);
            if (trainer != null && VerifyPassword(trainer.Password, password))
                return (true, GenerateToken(login, "Trainer"), "Trainer", "Успешный вход");

            var admin = await this._adminRepository.GetAdminByLoginAsync(login);
            if (admin != null && VerifyPassword(admin.Password, password))
                return (true, GenerateToken(login, admin.Status), admin.Status, "Успешный вход");

            return (false, null, null, "Неверный логин или пароль.");
        }

        private bool VerifyPassword(string hashedPassword, string password)
        {
            var dummy = new object();
            var result = _passwordHasher.VerifyHashedPassword(dummy, hashedPassword, password);
            if (result != PasswordVerificationResult.Success)
                Thread.Sleep(200);
            return result == PasswordVerificationResult.Success;
        }

        private string GenerateToken(string login, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

