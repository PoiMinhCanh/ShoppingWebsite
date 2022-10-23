using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingWebsite.Migrations
{
    public partial class ChangeProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Product",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "QuantityPerUnit",
                table: "Product",
                newName: "Quantity");

            migrationBuilder.AlterColumn<string>(
                name: "ProductDescription",
                table: "Product",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Product",
                newName: "QuantityPerUnit");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Product",
                newName: "UnitPrice");

            migrationBuilder.AlterColumn<string>(
                name: "ProductDescription",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}
