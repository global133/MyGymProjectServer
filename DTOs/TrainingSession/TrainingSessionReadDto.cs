using MyGymProject.Server.DTOs.Client;

namespace MyGymProject.Server.DTOs.TrainingSession
{
    public class TrainingSessionReadDto
    {
        public int Id { get; set; }
        public string TrainingName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TrainerName { get; set; } = null!;
        public string HallName { get; set; } = null!;
        public List<ClientReadDto> Clients { get; set; } = new();
    }
}
