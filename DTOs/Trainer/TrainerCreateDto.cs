using System.ComponentModel.DataAnnotations;

namespace MyGymProject.Server.DTOs.Trainer
{
    public class TrainerCreateDto
    {
        [Required(ErrorMessage = "Полное имя обязательно")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Дата рождения обязательна")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Телефон обязателен")]
        [Phone(ErrorMessage = "Некорректный формат номера телефона")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Пол обязателен")]
        public string Gender { get; set; } = null!;

        [Required(ErrorMessage = "Статус обязателен")]
        public string Status { get; set; } = null!;

        [Required(ErrorMessage = "Специализация обязательна")]
        public string Specialization { get; set; } = null!;

        [Required(ErrorMessage = "Часы работы обязательны")]
        public string WorkingHours { get; set; } = null!;

        [Required(ErrorMessage = "Логин обязателен")]
        [MinLength(4, ErrorMessage = "Логин должен содержать минимум 4 символа")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов")]
        public string Password { get; set; } = null!;
    }
}
