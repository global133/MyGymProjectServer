using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyGymProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_TrainingSessions_TrainingSessionId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_TrainingSessionId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TrainingSessionId",
                table: "Clients");

            migrationBuilder.CreateTable(
                name: "ClientTrainingSession",
                columns: table => new
                {
                    ClientsId = table.Column<int>(type: "integer", nullable: false),
                    TrainingSessionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTrainingSession", x => new { x.ClientsId, x.TrainingSessionsId });
                    table.ForeignKey(
                        name: "FK_ClientTrainingSession_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientTrainingSession_TrainingSessions_TrainingSessionsId",
                        column: x => x.TrainingSessionsId,
                        principalTable: "TrainingSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientTrainingSession_TrainingSessionsId",
                table: "ClientTrainingSession",
                column: "TrainingSessionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientTrainingSession");

            migrationBuilder.AddColumn<int>(
                name: "TrainingSessionId",
                table: "Clients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TrainingSessionId",
                table: "Clients",
                column: "TrainingSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_TrainingSessions_TrainingSessionId",
                table: "Clients",
                column: "TrainingSessionId",
                principalTable: "TrainingSessions",
                principalColumn: "Id");
        }
    }
}
