using MyGymProject.Server.DTOs.Client;

namespace MyGymProject.Server.DTOs.TrainingSession
{
    public class TrainingSessionReadDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MaxParticipants { get; set; }
        public int TrainingId { get; set; }
        public List<ClientReadDto> Clients { get; set; } = new();
    }
}
