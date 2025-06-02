namespace MyGymProject.Server.DTOs.Hall
{
    public class HallDtoResponse
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public List<string>? Trainings { get; set; }      
    }
}
