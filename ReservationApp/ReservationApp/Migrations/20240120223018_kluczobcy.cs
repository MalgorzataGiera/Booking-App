using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReservationApp.Migrations
{
    /// <inheritdoc />
    public partial class kluczobcy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameColumn(
                name: "Room",
                table: "Reservations",
                newName: "RoomId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "0350047e-33f7-4e7c-9d46-6c859bb5732f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "889c2db3-81ce-4801-9b8c-9e43799153f9");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "072192f4-1346-405d-8458-eacd240c8107", 0, "12905988-9627-479d-a9a6-423cf23e5434", "admin1@example.com", true, false, null, "ADMIN1@EXAMPLE.COM", "ADMIN1@EXAMPLE.COM", "AQAAAAEAACcQAAAAEIqr1DIJtdy9E+Scuzt3NoBmfftMKgi9Cbj3pDMC+oQ+uTtFMduy9yRUdFM1sOtRPg==", null, false, "d06e12f4-2a27-456b-860f-e72051be1f03", false, "admin1@example.com" },
                    { "209d0787-0d6e-4f04-84b8-a1d969670681", 0, "86bf2243-fdc2-4c95-bb94-a88fd4c3a2ea", "user1@example.com", true, false, null, "USER@EXAMPLE.COM", "USER1@EXAMPLE.COM", "AQAAAAEAACcQAAAAEMdhr7o1GGUPZOYVfVF9cM9DgwhLNxAMvWr0RLR0RNLQZaafH1Xg2wSiHCa+BsdU2Q==", null, false, "7f8bbde2-4e6c-424a-9c0c-661d3ac85c27", false, "user1@example.com" }
                });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "ReceivedById", "RoomId" },
                values: new object[] { new DateTime(2024, 1, 20, 23, 30, 18, 700, DateTimeKind.Local).AddTicks(7790), "072192f4-1346-405d-8458-eacd240c8107", 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "072192f4-1346-405d-8458-eacd240c8107" },
                    { "2", "209d0787-0d6e-4f04-84b8-a1d969670681" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Room_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Room_RoomId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "072192f4-1346-405d-8458-eacd240c8107" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "209d0787-0d6e-4f04-84b8-a1d969670681" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "072192f4-1346-405d-8458-eacd240c8107");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "209d0787-0d6e-4f04-84b8-a1d969670681");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Reservations",
                newName: "Room");

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
                columns: new[] { "Date", "ReceivedById", "Room" },
                values: new object[] { new DateTime(2024, 1, 20, 23, 24, 11, 85, DateTimeKind.Local).AddTicks(3279), "f1a1fdba-360d-4098-9928-52abd032d1c3", 101 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "78f406b9-0be2-40e3-8af2-7ca8738285ea" },
                    { "1", "f1a1fdba-360d-4098-9928-52abd032d1c3" }
                });
        }
    }
}
