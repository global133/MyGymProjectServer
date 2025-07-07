using MyGymProject.Server.DTOs.Client;
using System.ComponentModel.DataAnnotations;

namespace MyGymProject.Server.DTOs.TrainingSession
{
    public class TrainingSessionCreateDto
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

    }
}
