using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectARTEMIS.Migrations
{
    /// <inheritdoc />
    public partial class SeedSocials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SocialMedias",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("f1198a5b-2cc7-83fa-cd05-8f674afb05bf"), "Facebook" },
                    { new Guid("f22a9b6c-3dd8-94fa-de06-9f785b0c16cf"), "X (Twitter)" },
                    { new Guid("f33bc77d-4ee9-05fa-ef07-af896c1d27df"), "Discord" },
                    { new Guid("f44cd88e-5ff0-16fa-f008-bf9a7d2e38ef"), "Instagram" },
                    { new Guid("f55de99f-6fa1-27fa-f109-cfab8e3f49ff"), "TikTok" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: new Guid("f1198a5b-2cc7-83fa-cd05-8f674afb05bf"));

            migrationBuilder.DeleteData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: new Guid("f22a9b6c-3dd8-94fa-de06-9f785b0c16cf"));

            migrationBuilder.DeleteData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: new Guid("f33bc77d-4ee9-05fa-ef07-af896c1d27df"));

            migrationBuilder.DeleteData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: new Guid("f44cd88e-5ff0-16fa-f008-bf9a7d2e38ef"));

            migrationBuilder.DeleteData(
                table: "SocialMedias",
                keyColumn: "Id",
                keyValue: new Guid("f55de99f-6fa1-27fa-f109-cfab8e3f49ff"));
        }
    }
}
