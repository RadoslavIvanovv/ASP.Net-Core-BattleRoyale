using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleRoyale.Data.Migrations
{
    public partial class UpdatedPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Players_PlayerId1",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_PlayerId1",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "PlayerId1",
                table: "Heroes");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Players",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlayerId",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_PlayerId",
                table: "Heroes",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Players_PlayerId",
                table: "Heroes",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Players_PlayerId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_PlayerId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Heroes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerId1",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_PlayerId1",
                table: "Heroes",
                column: "PlayerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Players_PlayerId1",
                table: "Heroes",
                column: "PlayerId1",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
