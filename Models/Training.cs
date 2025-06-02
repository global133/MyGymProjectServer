namespace MyGymProject.Server.Models
{
    public class Training
    {
        public int Id { get; set; }

        public bool IsGroup { get; set; } 

        public DateTime Time { get; set; }

        public Trainer Trainer { get; set; } = null!;

        public Hall Hall { get; set; } = null!;

        public List<Client> Clients { get; set; } = new List<Client>();
    }
}
