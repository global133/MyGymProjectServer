using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyGymProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddImgPathField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Trainers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Trainers");
        }
    }
}
