using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Coupon.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitCoopon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coopons",
                columns: table => new
                {
                    CouponId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountAmount = table.Column<double>(type: "float", nullable: false),
                    MinAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coopons", x => x.CouponId);
                });

            migrationBuilder.InsertData(
                table: "Coopons",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[,]
                {
                    { 1, "JB007", 10.0, 120 },
                    { 2, "JB009", 20.0, 180 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coopons");
        }
    }
}
