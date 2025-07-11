namespace MyGymProject.Server.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? Description { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Status { get; set; }  
        public string Specialization { get; set; } = null!; 
        public string WorkingHours { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<TrainingSession> Trainings { get; set; } = new List<TrainingSession>();
    }
}
