using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleRoyale.Data.Migrations
{
    public partial class RemovedImageFromItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
