using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataGuncelleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "14686036-7975-4089-9a74-972161741544", "a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25f97fa9-5b6c-47f2-98b8-d808e28f378e", "AQAAAAIAAYagAAAAEOyak53cDr+qFBi1BHbBg617Ak4vJf2rsbs3JGPP5MUArt9r+ixBirk+ghl1jnxgcw==", "4443492f-21b3-4d59-b66b-f1b1ec2ada26" });

            migrationBuilder.InsertData(
                table: "Gyms",
                columns: new[] { "Id", "Address", "ClosingTime", "Name", "OpenFriday", "OpenMonday", "OpenSaturday", "OpenSunday", "OpenThursday", "OpenTuesday", "OpenWednesday", "OpeningTime" },
                values: new object[,]
                {
                    { 1, "Esentepe Kampüsü", new TimeSpan(0, 22, 0, 0, 0), "SAU Merkez Kampüs", true, true, false, false, true, true, true, new TimeSpan(0, 8, 0, 0, 0) },
                    { 2, "Mavi Durak Mevkii", new TimeSpan(0, 23, 0, 0, 0), "Serdivan Premium", true, true, false, false, true, true, true, new TimeSpan(0, 7, 0, 0, 0) },
                    { 3, "Adapazarı Merkez", new TimeSpan(0, 21, 0, 0, 0), "Çarşı Şube", true, true, false, false, true, true, true, new TimeSpan(0, 9, 0, 0, 0) },
                    { 4, "Erenler Merkez", new TimeSpan(0, 22, 30, 0, 0), "Erenler Kompleksi", true, true, false, false, true, true, true, new TimeSpan(0, 8, 30, 0, 0) },
                    { 5, "Sapanca Sahil", new TimeSpan(0, 22, 0, 0, 0), "Sapanca Life", true, true, false, false, true, true, true, new TimeSpan(0, 7, 30, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "DurationMinutes", "GymId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 60, 1, "Fitness Standart", 500m },
                    { 2, 45, 2, "Birebir Pilates", 1200m },
                    { 3, 50, 1, "Crossfit Seansı", 800m },
                    { 4, 90, 3, "Kick Boks Eğitimi", 1000m },
                    { 5, 75, 2, "Yoga ve Meditasyon", 700m },
                    { 6, 60, 4, "Yüzme Kursu", 900m },
                    { 7, 45, 3, "Zumba Grup Dersi", 400m },
                    { 8, 30, 5, "HIIT Antrenman", 600m },
                    { 9, 30, 1, "Diyetisyen Danışmanlığı", 1100m },
                    { 10, 120, 5, "VIP Spa & Fitness", 2500m }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "Bio", "GymId", "ImagePath", "Name", "ShiftEndTime", "ShiftStartTime", "Specialty", "WorkFriday", "WorkMonday", "WorkSaturday", "WorkSunday", "WorkThursday", "WorkTuesday", "WorkWednesday" },
                values: new object[,]
                {
                    { 1, null, 1, null, "Ahmet Yılmaz", new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), "Vücut Geliştirme", true, true, false, false, true, true, true },
                    { 2, null, 2, null, "Ayşe Demir", new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), "Pilates", true, true, false, false, true, true, true },
                    { 3, null, 1, null, "Mehmet Kaya", new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 13, 0, 0, 0), "Crossfit", true, true, false, false, true, true, true },
                    { 4, null, 2, null, "Selin Yurt", new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), "Yoga", true, true, false, false, true, true, true },
                    { 5, null, 4, null, "Caner Öz", new TimeSpan(0, 16, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), "Yüzme", true, true, false, false, true, true, true },
                    { 6, null, 3, null, "Elif Ak", new TimeSpan(0, 21, 0, 0, 0), new TimeSpan(0, 12, 0, 0, 0), "Kick Boks", true, true, false, false, true, true, true },
                    { 7, null, 5, null, "Burak Arslan", new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 14, 0, 0, 0), "Fitness", true, true, false, false, true, true, true },
                    { 8, null, 4, null, "Deniz Can", new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), "Kardiyo", true, true, false, false, true, true, true },
                    { 9, null, 1, null, "Murat Yıldız", new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), "Diyetisyen", true, true, false, false, true, true, true },
                    { 10, null, 3, null, "Fatma Şahin", new TimeSpan(0, 21, 0, 0, 0), new TimeSpan(0, 15, 0, 0, 0), "Zumba", true, true, false, false, true, true, true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14686036-7975-4089-9a74-972161741544",
                column: "ConcurrencyStamp",
                value: "c08536f1-c4da-4f31-9655-7960442c1a74");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "543c3933-289b-4401-831e-9279185a5382",
                column: "ConcurrencyStamp",
                value: "c578f0b3-11db-48ef-9221-5f7129d9e7eb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6e522967-832d-4581-9b62-302306915f01",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "270a97ce-fdfe-4e62-be88-53ae2c890d14", "AQAAAAIAAYagAAAAEOikLEF0R08RdYl0LSaoMvy3Ifyam9sol1N7c7zyIyUDCLXALg0/r+hTaTlCKOJG2A==", "47f7d9ff-4085-4a79-b557-646c3ffeb348" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d", 0, "c281e1d6-540b-4f6e-9ae2-194a0eb39692", "uye@sakarya.edu.tr", true, false, null, "UYE@SAKARYA.EDU.TR", "UYE@SAKARYA.EDU.TR", "AQAAAAIAAYagAAAAEKwucv1bj9+QU/kIv/jOjjjl/4lpZKYkEbVRAlt/MhOeNzIfW/r2rLRxrdmi3Oezgg==", null, false, "e67fcb46-78a2-4695-b836-bdaef11e439e", false, "uye@sakarya.edu.tr" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "14686036-7975-4089-9a74-972161741544", "a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d" });
        }
    }
}
