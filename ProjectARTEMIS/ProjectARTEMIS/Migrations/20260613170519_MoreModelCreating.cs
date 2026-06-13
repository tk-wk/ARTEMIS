using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectARTEMIS.Migrations
{
    /// <inheritdoc />
    public partial class MoreModelCreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PlayerProfiles",
                columns: new[] { "Id", "Bio", "InGameName", "Note", "ProfilePicturePath", "SchoolId", "UserId" },
                values: new object[] { new Guid("b2265d2e-9ff4-50ed-9b82-7d3417e9828d"), "Project ARTEMIS Head System Administrator.", "Spinelly", "", "", new Guid("44444444-4444-4444-4444-444444444444"), new Guid("a1154c1d-8ee3-49dc-8a71-6c2306d8717c") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsAdmin", "PasswordHash", "Username" },
                values: new object[] { new Guid("a1154c1d-8ee3-49dc-8a71-6c2306d8717c"), true, "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LJZdL17lhWy", "Spinelly" });

            migrationBuilder.InsertData(
                table: "PlayerOnlineStatuses",
                columns: new[] { "Id", "EndTime", "PlayerProfileId", "StartTime", "Status" },
                values: new object[] { new Guid("d4487f4a-1bb6-72fa-bd04-9f5639fb04af"), null, new Guid("b2265d2e-9ff4-50ed-9b82-7d3417e9828d"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 });

            migrationBuilder.InsertData(
                table: "PlayerProfileStatuses",
                columns: new[] { "Id", "EndTime", "Message", "PlayerProfileId", "StartTime", "Status" },
                values: new object[] { new Guid("c3376e3f-0aa5-61fe-ac93-8e4528fa939e"), null, "Profile initialized.", new Guid("b2265d2e-9ff4-50ed-9b82-7d3417e9828d"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlayerOnlineStatuses",
                keyColumn: "Id",
                keyValue: new Guid("d4487f4a-1bb6-72fa-bd04-9f5639fb04af"));

            migrationBuilder.DeleteData(
                table: "PlayerProfileStatuses",
                keyColumn: "Id",
                keyValue: new Guid("c3376e3f-0aa5-61fe-ac93-8e4528fa939e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1154c1d-8ee3-49dc-8a71-6c2306d8717c"));

            migrationBuilder.DeleteData(
                table: "PlayerProfiles",
                keyColumn: "Id",
                keyValue: new Guid("b2265d2e-9ff4-50ed-9b82-7d3417e9828d"));
        }
    }
}
