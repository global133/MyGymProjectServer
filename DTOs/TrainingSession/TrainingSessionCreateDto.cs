using MyGymProject.Server.DTOs.Client;
using System.ComponentModel.DataAnnotations;

namespace MyGymProject.Server.DTOs.TrainingSession
{
    public class TrainingSessionCreateDto
    {
        public string Name { get; set; } = null!;
        public bool IsGroup { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TrainerId { get; set; }
        public int HallId { get; set; }
    }
}
