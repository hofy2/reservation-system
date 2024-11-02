using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Trips",
                newName: "decimal(18,2)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "decimal(18,2)",
                table: "Trips",
                newName: "Price");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dd1c4200-790a-49b7-885f-deff56212c19", "AQAAAAIAAYagAAAAEK4QC3XO/avlFnD/HQUnrMRTU6WTz3tAl/u6M2/VBTvpWK2yn8OZtzhGFnOoliwUlw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8b7815c1-273d-444d-801b-babd0d06d525", "AQAAAAIAAYagAAAAEMV5BnZksacLT0nN+c1QEZoe10FWgPKPSflIp+6WubZ/6FUtg5sOOFwp+jP86VKJAQ==" });
        }
    }
}
