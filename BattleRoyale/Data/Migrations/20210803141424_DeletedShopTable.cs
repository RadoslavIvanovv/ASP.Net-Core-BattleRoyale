using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleRoyale.Data.Migrations
{
    public partial class DeletedShopTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Shops_ShopId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Items_ShopId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ShopId",
                table: "Items",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Shops_ShopId",
                table: "Items",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
