using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.DataAccess.Migrations
{
    public partial class InitDbVer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "ProductItems");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Users",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true,
                defaultValue: "[\"no_img.png\"]");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Suppliers",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true,
                defaultValue: "[\"no_img.png\"]");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Products",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true,
                defaultValue: "[\"no_img.png\"]");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "ProductItems",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true,
                defaultValue: "[\"no_img.png\"]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "ProductItems");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "ProductItems",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true);
        }
    }
}
