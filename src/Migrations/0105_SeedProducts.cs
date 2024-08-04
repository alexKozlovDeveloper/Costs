using CostKeeper.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostKeeper.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var products = FileExtensions.LoadFromJsonFile<Product[]>("Logs\\Products.json");

            foreach (var p in products)
            {
                migrationBuilder.InsertData(
                    table: "Products",
                    columns: ["Id", "Description", "Category", "Tags", "Weight", "CaloriesPer100g"],
                    values: new object[,]
                    {
                        { p.Id, p.Description, p.Category, p.Tags, p.Weight, p.CaloriesPer100g },
                    });
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
