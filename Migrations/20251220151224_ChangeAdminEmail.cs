using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAdminEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14686036-7975-4089-9a74-972161741544",
                column: "ConcurrencyStamp",
                value: "9cc586f2-c6fa-4459-86c7-265c89e032f2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "543c3933-289b-4401-831e-9279185a5382",
                column: "ConcurrencyStamp",
                value: "fcde8529-7de5-4da2-9a9e-31476d4a696f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6e522967-832d-4581-9b62-302306915f01",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "febeb226-15a3-472b-9634-40471418f83c", "b231210055@sakarya.edu.tr", "B231210055@SAKARYA.EDU.TR", "B231210055@SAKARYA.EDU.TR", "AQAAAAIAAYagAAAAEINyo5HD8BF1+X91dgSO+ezcWK0HkyXPyfvyghUaOJ0Y3d0ZQGqPf+UZW/CHTHmDCA==", "772493e5-fe48-4c3d-94b3-9ecf6a2b5be0", "b231210055@sakarya.edu.tr" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14686036-7975-4089-9a74-972161741544",
                column: "ConcurrencyStamp",
                value: "0a8447cc-ff86-4b55-a91b-f9cfabc81850");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "543c3933-289b-4401-831e-9279185a5382",
                column: "ConcurrencyStamp",
                value: "8d285034-025a-4298-82b1-8cd061f138cc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6e522967-832d-4581-9b62-302306915f01",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "25f97fa9-5b6c-47f2-98b8-d808e28f378e", "g211210000@sakarya.edu.tr", "G211210000@SAKARYA.EDU.TR", "G211210000@SAKARYA.EDU.TR", "AQAAAAIAAYagAAAAEOyak53cDr+qFBi1BHbBg617Ak4vJf2rsbs3JGPP5MUArt9r+ixBirk+ghl1jnxgcw==", "4443492f-21b3-4d59-b66b-f1b1ec2ada26", "g211210000@sakarya.edu.tr" });
        }
    }
}
