﻿namespace MyGymProject.Server.Models
{
    public class Hall
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public List<TrainingSession> Trainings {get; set;} = new List<TrainingSession>();
    }
}
