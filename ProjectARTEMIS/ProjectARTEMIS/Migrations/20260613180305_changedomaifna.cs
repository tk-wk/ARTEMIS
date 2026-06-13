using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectARTEMIS.Migrations
{
    /// <inheritdoc />
    public partial class changedomaifna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InGameName",
                table: "PlayerProfiles",
                newName: "RealName");

            migrationBuilder.UpdateData(
                table: "PlayerProfiles",
                keyColumn: "Id",
                keyValue: new Guid("b2265d2e-9ff4-50ed-9b82-7d3417e9828d"),
                column: "RealName",
                value: "Alex Sam Cabildo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RealName",
                table: "PlayerProfiles",
                newName: "InGameName");

            migrationBuilder.UpdateData(
                table: "PlayerProfiles",
                keyColumn: "Id",
                keyValue: new Guid("b2265d2e-9ff4-50ed-9b82-7d3417e9828d"),
                column: "InGameName",
                value: "Spinelly");
        }
    }
}
