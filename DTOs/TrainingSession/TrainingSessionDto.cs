using MyGymProject.Server.DTOs.Client;

namespace MyGymProject.Server.DTOs.TrainingSession
{
    public class TrainingSessionDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<ClientReadDto> BookedClients { get; set; } = new();
    }
}
