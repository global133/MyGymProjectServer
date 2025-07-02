using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyGymProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Trainings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Trainings");
        }
    }
}
