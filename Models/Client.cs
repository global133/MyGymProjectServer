namespace MyGymProject.Server.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
        public List<Training> Trainings { get; set; } = new List<Training>();
    }
}
