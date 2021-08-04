using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleRoyale.Data.Migrations
{
    public partial class AddedAdditionalEffectForItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdditionalEffect",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalEffect",
                table: "Items");
        }
    }
}
