using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a104e10a-0d4e-4c47-8208-c2ebbf47efbf", "AQAAAAIAAYagAAAAEFs2pKdjmkYtannkGrfERBap/dDKLUNOECWvgud4YuOxXN0d2loW4OTpJCsE5npggw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9297db60-040a-4574-ba26-7de00100722c", "AQAAAAIAAYagAAAAEB1UOd6mNxRzup/HLOiy4DwX42pPQm83BDotI2LO3wptuJn4VqA+1JEx2/fMFkmWsw==" });
        }
    }
}
