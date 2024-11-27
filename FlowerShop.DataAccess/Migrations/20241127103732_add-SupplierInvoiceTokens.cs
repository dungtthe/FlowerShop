using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.DataAccess.Migrations
{
    public partial class addSupplierInvoiceTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "SupplierInvoices",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierInvoiceTokenId",
                table: "SupplierInvoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SupplierInvoiceTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenEmail = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TokenAccept = table.Column<string>(type: "nvarchar(1006)", maxLength: 1006, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupplierInvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierInvoiceTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierInvoiceTokens_SupplierInvoices_SupplierInvoiceId",
                        column: x => x.SupplierInvoiceId,
                        principalTable: "SupplierInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInvoices_SupplierInvoiceTokenId",
                table: "SupplierInvoices",
                column: "SupplierInvoiceTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInvoiceTokens_SupplierInvoiceId",
                table: "SupplierInvoiceTokens",
                column: "SupplierInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierInvoices_SupplierInvoiceTokens_SupplierInvoiceTokenId",
                table: "SupplierInvoices",
                column: "SupplierInvoiceTokenId",
                principalTable: "SupplierInvoiceTokens",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierInvoices_SupplierInvoiceTokens_SupplierInvoiceTokenId",
                table: "SupplierInvoices");

            migrationBuilder.DropTable(
                name: "SupplierInvoiceTokens");

            migrationBuilder.DropIndex(
                name: "IX_SupplierInvoices_SupplierInvoiceTokenId",
                table: "SupplierInvoices");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SupplierInvoices");

            migrationBuilder.DropColumn(
                name: "SupplierInvoiceTokenId",
                table: "SupplierInvoices");
        }
    }
}
