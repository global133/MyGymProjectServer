namespace MyGymProject.Server.DTOs.Admin
{
    public class AdminReadDto
    {
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; } 

        public string Gender { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string Login { get; set; }
    }
}
