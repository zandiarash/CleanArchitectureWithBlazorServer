using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Migrations
{
    public partial class shippingorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GrossMargin",
                table: "ShippingOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "ShippingOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ShippingOrders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossMargin",
                table: "ShippingOrders");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "ShippingOrders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ShippingOrders");
        }
    }
}
