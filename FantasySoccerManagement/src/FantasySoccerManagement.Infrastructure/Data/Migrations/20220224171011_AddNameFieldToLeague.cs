using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasySoccerManagement.Infrastructure.Data.Migrations
{
    public partial class AddNameFieldToLeague : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Leagues",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Leagues");
        }
    }
}
