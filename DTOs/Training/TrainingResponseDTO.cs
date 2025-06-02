namespace MyGymProject.Server.DTOs.Training
{
    public class TrainingResponseDTO
    {
        public int Id { get; set; }
        public bool IsGroup { get; set; }
        public DateTime Time { get; set; }
        public string? TrainerName { get; set; }
        public string? HallName { get; set; }
        public List<string>? ClientNames { get; set; }
    }
}
