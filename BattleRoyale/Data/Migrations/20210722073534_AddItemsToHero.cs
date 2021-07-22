using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleRoyale.Data.Migrations
{
    public partial class AddItemsToHero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeroId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_HeroId",
                table: "Items",
                column: "HeroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Heroes_HeroId",
                table: "Items",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Heroes_HeroId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_HeroId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "HeroId",
                table: "Items");
        }
    }
}
