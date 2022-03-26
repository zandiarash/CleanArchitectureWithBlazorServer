using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Migrations
{
    public partial class Driver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Driver",
                table: "ShippingOrders",
                newName: "DriverName");

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "ShippingOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrithDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    DrivingNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrders_DriverId",
                table: "ShippingOrders",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrders_Drivers_DriverId",
                table: "ShippingOrders",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrders_Drivers_DriverId",
                table: "ShippingOrders");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_ShippingOrders_DriverId",
                table: "ShippingOrders");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "ShippingOrders");

            migrationBuilder.RenameColumn(
                name: "DriverName",
                table: "ShippingOrders",
                newName: "Driver");
        }
    }
}
