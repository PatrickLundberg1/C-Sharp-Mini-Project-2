using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CSharpMiniProject2.Migrations
{
    /// <inheritdoc />
    public partial class SetupDBs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "computers",
                columns: new[] { "Id", "Brand", "Model", "Office", "Price", "Purchase_date" },
                values: new object[,]
                {
                    { 1, "HP", "Elitebook", "Spain", 1423, new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "HP", "Elitebook", "Sweden", 588, new DateTime(2019, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Asus", "W234", "USA", 1200, new DateTime(2019, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Lenovo", "Yoga 730", "USA", 835, new DateTime(2019, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Lenovo", "Yoga 530", "USA", 1030, new DateTime(2019, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "phones",
                columns: new[] { "Id", "Brand", "Model", "Office", "Price", "Purchase_date" },
                values: new object[,]
                {
                    { 1, "iPhone", "8", "Spain", 970, new DateTime(2019, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "iPhone", "11", "Spain", 990, new DateTime(2022, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "iPhone", "X", "Sweden", 1245, new DateTime(2019, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Motorola", "Razr", "Sweden", 970, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "computers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "computers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "computers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "computers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "computers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "phones",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "phones",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "phones",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "phones",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
