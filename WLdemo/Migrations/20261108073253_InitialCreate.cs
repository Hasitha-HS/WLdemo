using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLdemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryAvgPrices",
                columns: table => new
                {
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "HighestStockValueCategories",
                columns: table => new
                {
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalStockValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_GetAveragePricePerCategory
                AS
                BEGIN
                    SELECT
                        Category,
                        CAST(AVG(Price) AS DECIMAL(18,2)) AS AveragePrice
                    FROM Products
                    GROUP BY Category;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_GetHighestStockValueCategory
                AS
                BEGIN
                    SELECT TOP 1
                        Category,
                        SUM(Price * Stock) AS TotalStockValue
                    FROM Products
                    GROUP BY Category
                    ORDER BY TotalStockValue DESC;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetAveragePricePerCategory");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetHighestStockValueCategory");

            migrationBuilder.DropTable(
                name: "CategoryAvgPrices");

            migrationBuilder.DropTable(
                name: "HighestStockValueCategories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
