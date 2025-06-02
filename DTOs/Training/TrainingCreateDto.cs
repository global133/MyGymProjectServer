using MyGymProject.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace MyGymProject.Server.DTOs.Training
{
    public class TrainingCreateDto
    {
        [Required (ErrorMessage = "Поле является ли тренировка групповой обязательно")]
        public bool IsGroup { get; set; }


        [Required (ErrorMessage = "Поле время обязательно")]
        public DateTime Time { get; set; }

        [Required (ErrorMessage = "Поле id тренира обязательно")]
        public int TrainerId { get; set; }

        [Required (ErrorMessage = "Поле id зала обязательно")]
        public int HallId { get; set; } 
    }
}
