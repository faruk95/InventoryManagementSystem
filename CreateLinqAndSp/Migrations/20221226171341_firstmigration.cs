using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreateLinqAndSp.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblItem",
                columns: table => new
                {
                    intItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strItemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    numStockQuantity = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblItem__FA6F1B123D9D4BA9", x => x.intItemId);
                });

            migrationBuilder.CreateTable(
                name: "tblPartner",
                columns: table => new
                {
                    intPartnerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strPartnerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    intPartnerTypeId = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblPartn__279F3038FA8FC6F3", x => x.intPartnerId);
                });

            migrationBuilder.CreateTable(
                name: "tblPartnerType",
                columns: table => new
                {
                    intPartnerTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strPartnerTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblPartn__353019536A6E1DCE", x => x.intPartnerTypeId);
                });

            migrationBuilder.CreateTable(
                name: "tblPurchase",
                columns: table => new
                {
                    intPurchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intSupplierId = table.Column<int>(type: "int", nullable: true),
                    dtePurchaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblPurch__39AFE6058A3D2FAE", x => x.intPurchaseId);
                });

            migrationBuilder.CreateTable(
                name: "tblPurchaseDetails",
                columns: table => new
                {
                    intDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intPurchaseId = table.Column<int>(type: "int", nullable: true),
                    intItemId = table.Column<int>(type: "int", nullable: true),
                    numItemQuantity = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    numUnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblPurch__0A1B5AF362E52B46", x => x.intDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "tblSales",
                columns: table => new
                {
                    intSalesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intCustomerId = table.Column<int>(type: "int", nullable: true),
                    dteSalesDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblSales__754F6C55A85CC1C6", x => x.intSalesId);
                });

            migrationBuilder.CreateTable(
                name: "tblSalesDetails",
                columns: table => new
                {
                    intDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intSalesId = table.Column<int>(type: "int", nullable: true),
                    intItemId = table.Column<int>(type: "int", nullable: true),
                    numItemQuantity = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    numUnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblSales__0A1B5AF32251E7AD", x => x.intDetailsId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblItem");

            migrationBuilder.DropTable(
                name: "tblPartner");

            migrationBuilder.DropTable(
                name: "tblPartnerType");

            migrationBuilder.DropTable(
                name: "tblPurchase");

            migrationBuilder.DropTable(
                name: "tblPurchaseDetails");

            migrationBuilder.DropTable(
                name: "tblSales");

            migrationBuilder.DropTable(
                name: "tblSalesDetails");
        }
    }
}
