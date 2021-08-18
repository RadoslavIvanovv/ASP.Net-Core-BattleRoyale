using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleRoyale.Data.Migrations
{
    public partial class AddedTotalExpForHeroes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalExperiencePoints",
                table: "Heroes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalExperiencePoints",
                table: "Heroes");
        }
    }
}
