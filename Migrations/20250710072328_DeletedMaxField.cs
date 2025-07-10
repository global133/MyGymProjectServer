using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyGymProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class DeletedMaxField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxParticipants",
                table: "TrainingSessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxParticipants",
                table: "TrainingSessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
