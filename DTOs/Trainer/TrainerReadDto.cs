namespace MyGymProject.Server.DTOs.Trainer
{
    public class TrainerReadDto
    {
        public string Login { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
