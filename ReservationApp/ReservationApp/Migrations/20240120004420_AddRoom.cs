using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReservationApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "083b28d7-c94b-4f6c-8193-8ef4144bb77d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "cd967b25-3793-47a9-b7a6-c8262ca13c69" });

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "083b28d7-c94b-4f6c-8193-8ef4144bb77d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd967b25-3793-47a9-b7a6-c8262ca13c69");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "ff6b62a8-1f05-46db-8915-1dabf586e68a", "Admin", "ADMIN" },
                    { "2", "2b5aef8b-a0a9-4060-9b1a-b1ac4ae30fc3", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "083b28d7-c94b-4f6c-8193-8ef4144bb77d", 0, "385bba49-3158-402f-9d7a-1b08a48ab160", "admin1@example.com", true, false, null, "ADMIN1@EXAMPLE.COM", "ADMIN1@EXAMPLE.COM", "AQAAAAEAACcQAAAAEPYiH9EjhhP3/rzqkY8J+mfIeJigPu2zAolw0ORNrhbSK6C4MnRAUAnJP4sipqJY2g==", null, false, "bf59c719-ed4a-4249-a7ce-a9c26e808501", false, "admin1@example.com" },
                    { "cd967b25-3793-47a9-b7a6-c8262ca13c69", 0, "4904e4c0-ace4-4187-b46a-48e66390345c", "user1@example.com", true, false, null, "USER@EXAMPLE.COM", "USER1@EXAMPLE.COM", "AQAAAAEAACcQAAAAEMQULgjRfEVTVuQV8gwBl2PO8hal1DZhFzw6nhKnS6ib1lyA1RJL3qhMl4+BVX9aRw==", null, false, "920ecd23-b510-4cc4-9acb-f60ca060475c", false, "user1@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "083b28d7-c94b-4f6c-8193-8ef4144bb77d" },
                    { "2", "cd967b25-3793-47a9-b7a6-c8262ca13c69" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "Address", "City", "Date", "NumberOfNights", "Owner", "Price", "ReceivedById", "Room" },
                values: new object[] { 1, "Sample Address", "Sample City", new DateTime(2024, 1, 16, 18, 36, 51, 599, DateTimeKind.Local).AddTicks(887), 3, "Sample Owner", 100.00m, "083b28d7-c94b-4f6c-8193-8ef4144bb77d", 101 });
        }
    }
}
