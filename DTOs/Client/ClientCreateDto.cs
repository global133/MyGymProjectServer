using MyGymProject.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace MyGymProject.Server.DTOs.Client
{
    public class ClientCreateDto
    {
        [Required(ErrorMessage = "Полное имя обязательно")]
        public string FullName { get; set; } = null!;


        [Required(ErrorMessage = "Дата рождения обязательна")]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Номер телефона обязателен")]
        [Phone(ErrorMessage = "Некорректный формат номера телефона")]
        public string Phone { get; set; } = null!;


        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Email { get; set; } = null!;


        [Required(ErrorMessage = "Пол обязателен")]
        public string Gender { get; set; } = null!;


        [Required(ErrorMessage = "Логин обязателен")]
        [MinLength(4, ErrorMessage = "Логин должен быть не короче 4 символов")]
        public string Login { get; set; } = null!;


        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(6, ErrorMessage = "Пароль должен быть не короче 6 символов")]
        public string Password { get; set; } = null!;
    }
}
