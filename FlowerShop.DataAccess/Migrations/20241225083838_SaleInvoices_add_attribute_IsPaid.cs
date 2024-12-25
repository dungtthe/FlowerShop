using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.DataAccess.Migrations
{
    public partial class SaleInvoices_add_attribute_IsPaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "SaleInvoices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "SaleInvoices");
        }
    }
}
