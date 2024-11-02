using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc862aea-6cf0-44ad-9e6e-e692f7633beb", "AQAAAAIAAYagAAAAEECEPciezRsquvGp50yhYqYELcvMB9YUO0RiUzALf0cGWVBEBWCqIoe4RFKod1hwrg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c7766ce7-391a-4d86-8a30-4e146a04fb15", "AQAAAAIAAYagAAAAEDuRxROUCjQx+HSOrm3Ivzl2+uIkw9xAq7e9y0E7A6ML19UBU3bTc6wmDtZleTCMKA==" });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "CityName", "Content", "CreationDate", "ImageUrl", "Name", "decimal(18,2)" },
                values: new object[,]
                {
                    { 3, "Paris", "<p>Beautiful trip to Paris</p>", new DateTime(2024, 1, 1, 2, 0, 0, 0, DateTimeKind.Local), "image_url", "Trip to Paris", 1000m },
                    { 4, "Rome", "<p>Amazing trip to Rome</p>", new DateTime(2024, 1, 2, 2, 0, 0, 0, DateTimeKind.Local), "image_url", "Trip to Rome", 1200m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bbc65dfe-705a-4694-9fb3-8adfd5a1535d", "AQAAAAIAAYagAAAAELMngG9lSUly9bHmuzGl18OxjDSnDBLlUhyCxMgkIdRornwscySmahU3Ai5HoPHEGA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "90311a50-aead-42d6-a42a-56f08646c59b", "AQAAAAIAAYagAAAAELv3/oPF1Vz1nNrz8RClLkGbUugMXJCmJ/IuErNS9WShS2wkbD8xqfAm9M8IrAtgZg==" });
        }
    }
}
