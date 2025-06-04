namespace MyGymProject.Server.DTOs.Client
{
    public class ClientReadDto
    {
        public string Login { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime DateOfBirth { get; set; } 
    }
}
