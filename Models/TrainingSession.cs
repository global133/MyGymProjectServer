namespace MyGymProject.Server.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MaxParticipants { get; set; }
        public Training Training { get; set; }
        public List<Client> Clients { get; set; } = new List<Client>();
    }
}
