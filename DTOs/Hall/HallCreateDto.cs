using System.ComponentModel.DataAnnotations;

namespace MyGymProject.Server.DTOs.Hall
{
    public class HallCreateDto
    {
        [Required (ErrorMessage = "Название зала обязательно")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Описание зала обязательно")]
        public string Description { get; set; } = null!;
    }
}
