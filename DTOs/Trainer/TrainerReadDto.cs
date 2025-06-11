namespace MyGymProject.Server.DTOs.Trainer
{
    public class TrainerReadDto
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? Description { get; set; }
        public string Phone { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public int Age =>
        DateTime.Now.Year - DateOfBirth.Year -
        (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
    }
}
