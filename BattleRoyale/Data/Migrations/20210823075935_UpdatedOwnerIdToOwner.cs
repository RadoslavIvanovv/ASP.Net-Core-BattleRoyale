using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleRoyale.Data.Migrations
{
    public partial class UpdatedOwnerIdToOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemOwner",
                table: "AuctionItems");

            migrationBuilder.AddColumn<string>(
                name: "ItemOwnerId",
                table: "AuctionItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItems_ItemOwnerId",
                table: "AuctionItems",
                column: "ItemOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionItems_Players_ItemOwnerId",
                table: "AuctionItems",
                column: "ItemOwnerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionItems_Players_ItemOwnerId",
                table: "AuctionItems");

            migrationBuilder.DropIndex(
                name: "IX_AuctionItems_ItemOwnerId",
                table: "AuctionItems");

            migrationBuilder.DropColumn(
                name: "ItemOwnerId",
                table: "AuctionItems");

            migrationBuilder.AddColumn<string>(
                name: "ItemOwner",
                table: "AuctionItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
