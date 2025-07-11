namespace MyGymProject.Server.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsGroup { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Trainer Trainer { get; set; } = null!;
        public Hall Hall { get; set; } = null!;
        public List<Client> Clients { get; set; } = new List<Client>();
    }
}
