using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasySoccerManagement.Infrastructure.Data.Migrations
{
    public partial class AddForeignKeyToTransferEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Leagues_LeagueId",
                table: "Transfers");

            migrationBuilder.AlterColumn<Guid>(
                name: "LeagueId",
                table: "Transfers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Leagues_LeagueId",
                table: "Transfers",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Leagues_LeagueId",
                table: "Transfers");

            migrationBuilder.AlterColumn<Guid>(
                name: "LeagueId",
                table: "Transfers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Leagues_LeagueId",
                table: "Transfers",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id");
        }
    }
}
