using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.DataAccess.Migrations
{
    public partial class InitDbVer4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "SupplierInvoices",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "SaleInvoices",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "SupplierInvoices");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "SaleInvoices");
        }
    }
}
