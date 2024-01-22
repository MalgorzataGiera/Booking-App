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
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "083b28d7-c94b-4f6c-8193-8ef4144bb77d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd967b25-3793-47a9-b7a6-c8262ca13c69");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "ff6b62a8-1f05-46db-8915-1dabf586e68a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "2b5aef8b-a0a9-4060-9b1a-b1ac4ae30fc3");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "083b28d7-c94b-4f6c-8193-8ef4144bb77d", 0, "385bba49-3158-402f-9d7a-1b08a48ab160", "admin1@example.com", true, false, null, "ADMIN1@EXAMPLE.COM", "ADMIN1@EXAMPLE.COM", "AQAAAAEAACcQAAAAEPYiH9EjhhP3/rzqkY8J+mfIeJigPu2zAolw0ORNrhbSK6C4MnRAUAnJP4sipqJY2g==", null, false, "bf59c719-ed4a-4249-a7ce-a9c26e808501", false, "admin1@example.com" },
                    { "cd967b25-3793-47a9-b7a6-c8262ca13c69", 0, "4904e4c0-ace4-4187-b46a-48e66390345c", "user1@example.com", true, false, null, "USER@EXAMPLE.COM", "USER1@EXAMPLE.COM", "AQAAAAEAACcQAAAAEMQULgjRfEVTVuQV8gwBl2PO8hal1DZhFzw6nhKnS6ib1lyA1RJL3qhMl4+BVX9aRw==", null, false, "920ecd23-b510-4cc4-9acb-f60ca060475c", false, "user1@example.com" }
                });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "ReceivedById" },
                values: new object[] { new DateTime(2024, 1, 16, 18, 36, 51, 599, DateTimeKind.Local).AddTicks(887), "083b28d7-c94b-4f6c-8193-8ef4144bb77d" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "083b28d7-c94b-4f6c-8193-8ef4144bb77d" },
                    { "2", "cd967b25-3793-47a9-b7a6-c8262ca13c69" }
                });
        }
    }
}
