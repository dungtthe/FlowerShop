using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.DataAccess.Migrations
{
    public partial class add_attribute_ShippingCostPerKilometer_In_ParameterConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShippingCostPerKilometer",
                table: "ParameterConfigurations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingCostPerKilometer",
                table: "ParameterConfigurations");
        }
    }
}
