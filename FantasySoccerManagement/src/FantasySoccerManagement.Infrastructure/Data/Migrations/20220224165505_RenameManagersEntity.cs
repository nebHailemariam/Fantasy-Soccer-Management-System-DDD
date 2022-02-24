using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasySoccerManagement.Infrastructure.Data.Migrations
{
    public partial class RenameManagersEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamManagers_Managers_ManagersId",
                table: "TeamManagers");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.RenameColumn(
                name: "ManagersId",
                table: "TeamManagers",
                newName: "LeagueId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamManagers_ManagersId",
                table: "TeamManagers",
                newName: "IX_TeamManagers_LeagueId");

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TeamManagers_Leagues_LeagueId",
                table: "TeamManagers",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamManagers_Leagues_LeagueId",
                table: "TeamManagers");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.RenameColumn(
                name: "LeagueId",
                table: "TeamManagers",
                newName: "ManagersId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamManagers_LeagueId",
                table: "TeamManagers",
                newName: "IX_TeamManagers_ManagersId");

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TeamManagers_Managers_ManagersId",
                table: "TeamManagers",
                column: "ManagersId",
                principalTable: "Managers",
                principalColumn: "Id");
        }
    }
}
