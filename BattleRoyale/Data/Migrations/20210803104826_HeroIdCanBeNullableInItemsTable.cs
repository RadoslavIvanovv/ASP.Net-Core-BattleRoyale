using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleRoyale.Data.Migrations
{
    public partial class HeroIdCanBeNullableInItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Heroes_HeroId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "HeroId",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "HeroId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Heroes_HeroId",
                table: "Items",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
