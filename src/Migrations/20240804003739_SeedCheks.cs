using CostKeeper.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostKeeper.Migrations
{
    /// <inheritdoc />
    public partial class SeedCheks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var products = FileExtensions.LoadFromJsonFile<Check[]>("Logs\\AugustChecks.json");

            foreach (var p in products)
            {
                migrationBuilder.InsertData(
                    table: "Checks",
                    columns: ["Id", "ProductId", "Date", "Price", "Count"],
                    values: new object[,]
                    {
                        { p.Id, p.ProductId, p.Date, p.Price, p.Count },
                    });
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
