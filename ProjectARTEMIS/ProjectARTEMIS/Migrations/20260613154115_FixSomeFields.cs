using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectARTEMIS.Migrations
{
    /// <inheritdoc />
    public partial class FixSomeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMediaHandles_PlayerProfiles_PlayerProfileId",
                table: "SocialMediaHandles");

            migrationBuilder.DropForeignKey(
                name: "FK_WhitelistRequestStatuses_WhitelistRequests_WhitelistRequest~",
                table: "WhitelistRequestStatuses");

            migrationBuilder.DropColumn(
                name: "RegistrationRequestId",
                table: "WhitelistRequestStatuses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SocialMediaHandles");

            migrationBuilder.AlterColumn<Guid>(
                name: "WhitelistRequestId",
                table: "WhitelistRequestStatuses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerProfileId",
                table: "SocialMediaHandles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "PlayerProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMediaHandles_PlayerProfiles_PlayerProfileId",
                table: "SocialMediaHandles",
                column: "PlayerProfileId",
                principalTable: "PlayerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhitelistRequestStatuses_WhitelistRequests_WhitelistRequest~",
                table: "WhitelistRequestStatuses",
                column: "WhitelistRequestId",
                principalTable: "WhitelistRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMediaHandles_PlayerProfiles_PlayerProfileId",
                table: "SocialMediaHandles");

            migrationBuilder.DropForeignKey(
                name: "FK_WhitelistRequestStatuses_WhitelistRequests_WhitelistRequest~",
                table: "WhitelistRequestStatuses");

            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "PlayerProfiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "WhitelistRequestId",
                table: "WhitelistRequestStatuses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "RegistrationRequestId",
                table: "WhitelistRequestStatuses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerProfileId",
                table: "SocialMediaHandles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SocialMediaHandles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMediaHandles_PlayerProfiles_PlayerProfileId",
                table: "SocialMediaHandles",
                column: "PlayerProfileId",
                principalTable: "PlayerProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WhitelistRequestStatuses_WhitelistRequests_WhitelistRequest~",
                table: "WhitelistRequestStatuses",
                column: "WhitelistRequestId",
                principalTable: "WhitelistRequests",
                principalColumn: "Id");
        }
    }
}
