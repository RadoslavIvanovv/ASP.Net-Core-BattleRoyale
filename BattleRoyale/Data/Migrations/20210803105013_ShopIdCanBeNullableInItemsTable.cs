using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleRoyale.Data.Migrations
{
    public partial class ShopIdCanBeNullableInItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Shops_ShopId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Shops_ShopId",
                table: "Items",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Shops_ShopId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Shops_ShopId",
                table: "Items",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
