using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasySoccerManagement.Infrastructure.Data.Migrations
{
    public partial class UpdateTeamAndTeamManagerRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamManagerId",
                table: "Teams");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamManagerId",
                table: "Teams",
                column: "TeamManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamManagerId",
                table: "Teams");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamManagerId",
                table: "Teams",
                column: "TeamManagerId",
                unique: true);
        }
    }
}
