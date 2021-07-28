using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleRoyale.Data.Migations
{
    public partial class UpdatedPetTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "HeroType",
                table: "Pets",
                newName: "Stats");

            migrationBuilder.RenameColumn(
                name: "Effect",
                table: "Pets",
                newName: "HeroId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_HeroId",
                table: "Pets",
                column: "HeroId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Heroes_HeroId",
                table: "Pets",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Heroes_HeroId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_HeroId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "Stats",
                table: "Pets",
                newName: "HeroType");

            migrationBuilder.RenameColumn(
                name: "HeroId",
                table: "Pets",
                newName: "Effect");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
