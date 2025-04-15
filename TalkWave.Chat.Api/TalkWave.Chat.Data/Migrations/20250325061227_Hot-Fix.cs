using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalkWave.Chat.Data.Migrations
{
    /// <inheritdoc />
    public partial class HotFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMembers_Users_UserEntityId",
                table: "ChatMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserEntityId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserEntityId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMembers_UserEntityId",
                table: "ChatMembers");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "ChatMembers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "Messages",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "ChatMembers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserEntityId",
                table: "Messages",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMembers_UserEntityId",
                table: "ChatMembers",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMembers_Users_UserEntityId",
                table: "ChatMembers",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserEntityId",
                table: "Messages",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
