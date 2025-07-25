using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entity_Framework_Core.Migrations
{
    /// <inheritdoc />
    public partial class twodatabases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Language_LanguageId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_BookPrices_Currencies_currenciesId",
                table: "BookPrices");

            migrationBuilder.DropIndex(
                name: "IX_BookPrices_currenciesId",
                table: "BookPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "currenciesId",
                table: "BookPrices");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "Languages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "CreatedOn", "Description", "IsActive", "LanguageId", "NoOfPages", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Local), "about ramayan", true, 1, 100, "ramayan" },
                    { 2, new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Local), " about mahabharath", true, 1, 100, "Mahabharath" },
                    { 3, new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Local), "about shiva ", true, 1, 100, "ShivTriology" },
                    { 4, new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Local), "about puranas", true, 1, 100, "Puranas" }
                });

            migrationBuilder.InsertData(
                table: "BookPrices",
                columns: new[] { "Id", "Amount", "BookId", "CurrencyId" },
                values: new object[,]
                {
                    { 1, 200, 1, 1 },
                    { 2, 300, 2, 2 },
                    { 3, 400, 3, 3 },
                    { 4, 500, 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookPrices_CurrencyId",
                table: "BookPrices",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Languages_LanguageId",
                table: "Book",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookPrices_Currencies_CurrencyId",
                table: "BookPrices",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Languages_LanguageId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_BookPrices_Currencies_CurrencyId",
                table: "BookPrices");

            migrationBuilder.DropIndex(
                name: "IX_BookPrices_CurrencyId",
                table: "BookPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DeleteData(
                table: "BookPrices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookPrices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookPrices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookPrices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Language");

            migrationBuilder.AddColumn<int>(
                name: "currenciesId",
                table: "BookPrices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BookPrices_currenciesId",
                table: "BookPrices",
                column: "currenciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Language_LanguageId",
                table: "Book",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookPrices_Currencies_currenciesId",
                table: "BookPrices",
                column: "currenciesId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
