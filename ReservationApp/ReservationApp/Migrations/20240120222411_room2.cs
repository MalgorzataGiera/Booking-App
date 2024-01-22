using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReservationApp.Migrations
{
    /// <inheritdoc />
    public partial class room2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "09f18106-2ac7-4c4a-8cde-172f86b5fcea" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "8415e2a8-6e1d-47f7-8ee2-6fa8b19b6d66" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "09f18106-2ac7-4c4a-8cde-172f86b5fcea");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8415e2a8-6e1d-47f7-8ee2-6fa8b19b6d66");

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "cca683d9-dd48-447e-bd27-4383331a7145");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "ac7be0da-389f-4a9e-89a0-a2517d873963");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "78f406b9-0be2-40e3-8af2-7ca8738285ea", 0, "2b23611c-63d5-4cc9-9f20-7b0458c664f8", "user1@example.com", true, false, null, "USER@EXAMPLE.COM", "USER1@EXAMPLE.COM", "AQAAAAEAACcQAAAAEPh6GCzE66OkVTtmGzMvwSaJYNqWkek/enI/7FJpMKkvMo0ZgBd6Oxl46CfjY4WWGA==", null, false, "5b4f7e3c-d728-4ac4-88cc-698ab001ab53", false, "user1@example.com" },
                    { "f1a1fdba-360d-4098-9928-52abd032d1c3", 0, "c91c4685-7156-4d1d-83e9-504536c6e05e", "admin1@example.com", true, false, null, "ADMIN1@EXAMPLE.COM", "ADMIN1@EXAMPLE.COM", "AQAAAAEAACcQAAAAEFAA1Xk8dYNEoC6YImwGf+MJIRGu41hf+WdSKvdYXRtrsAziaLe3bAF8o7r7MebpAA==", null, false, "fe3186cf-27b0-4432-a4d8-d0db0cfba56b", false, "admin1@example.com" }
                });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "ReceivedById" },
                values: new object[] { new DateTime(2024, 1, 20, 23, 24, 11, 85, DateTimeKind.Local).AddTicks(3279), "f1a1fdba-360d-4098-9928-52abd032d1c3" });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Price", "RoomNumber" },
                values: new object[] { 1, 100.00m, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "78f406b9-0be2-40e3-8af2-7ca8738285ea" },
                    { "1", "f1a1fdba-360d-4098-9928-52abd032d1c3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "78f406b9-0be2-40e3-8af2-7ca8738285ea" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "f1a1fdba-360d-4098-9928-52abd032d1c3" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78f406b9-0be2-40e3-8af2-7ca8738285ea");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f1a1fdba-360d-4098-9928-52abd032d1c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "ded09c1f-b4de-48ca-a31e-0db3821c0ddb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "bda1005b-e03e-432d-a676-2ea14b4e173d");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "09f18106-2ac7-4c4a-8cde-172f86b5fcea", 0, "fd58ce90-c649-47fe-acb9-6ca5adeaac08", "admin1@example.com", true, false, null, "ADMIN1@EXAMPLE.COM", "ADMIN1@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJJxBL2fy6ByyNYEfEV8siDYVhRXXnyGuR+pxvluna4cwMTq0mSe3Kc/UDpTZq3UWg==", null, false, "be1fddcd-aaaf-46a3-8ec4-9eaae93b76fc", false, "admin1@example.com" },
                    { "8415e2a8-6e1d-47f7-8ee2-6fa8b19b6d66", 0, "b5535117-634e-4425-9937-aec9b1c9b9dc", "user1@example.com", true, false, null, "USER@EXAMPLE.COM", "USER1@EXAMPLE.COM", "AQAAAAEAACcQAAAAEFe03O5YVQ6o+MF/DqwwLntVrN/c8+O8+LBP81nN7MP5+pZOLcuLuFjNBl/vltp83g==", null, false, "2c1082ad-9b87-4a87-911c-2a0b7737e4c5", false, "user1@example.com" }
                });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "ReceivedById" },
                values: new object[] { new DateTime(2024, 1, 20, 23, 20, 14, 390, DateTimeKind.Local).AddTicks(4812), "09f18106-2ac7-4c4a-8cde-172f86b5fcea" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "09f18106-2ac7-4c4a-8cde-172f86b5fcea" },
                    { "2", "8415e2a8-6e1d-47f7-8ee2-6fa8b19b6d66" }
                });
        }
    }
}
