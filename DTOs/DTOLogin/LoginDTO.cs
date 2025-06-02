using System.ComponentModel.DataAnnotations;

namespace MyGymProject.Server.DTOs.DTOLogin
{
    public class LoginDTO
    {
        [Required (ErrorMessage = "Логин обязателен")]
        public string Login { get; set; } = null!;

        [Required (ErrorMessage = "Пароль обязателен")]
        public string Password { get; set; } = null!;
    }
}
