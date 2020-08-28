using Microsoft.EntityFrameworkCore.Migrations;

namespace EzGame.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameId",
                table: "GameEditions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameEditions_GameId",
                table: "GameEditions",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameEditions_Games_GameId",
                table: "GameEditions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameEditions_Games_GameId",
                table: "GameEditions");

            migrationBuilder.DropIndex(
                name: "IX_GameEditions_GameId",
                table: "GameEditions");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GameEditions");
        }
    }
}
