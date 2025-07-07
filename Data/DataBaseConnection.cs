
using MyGymProject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MyGymProject.Server.Data
{
    public class DataBaseConnection : DbContext
    {
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Training> Trainings { get; set; }

        public DbSet<TrainingSession> TrainingSessions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=GymDataBase;Username=postgres;Password=1234");
        }
    }
}
