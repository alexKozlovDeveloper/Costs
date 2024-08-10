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
            var files = Directory.GetFiles("Data\\Checks\\August");

            var id = 1;
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var date = DateTime.Parse(fileName);
                // "Date": "2024-08-04T00:00:00.0000000Z"

                MigrationsExtensions.LoadFromJson<Check>(
                    migrationBuilder,
                    "Checks",
                    file,
                    ["Id", "ProductId", "Date", "Price", "Count"],
                    a => new object[,] { { id++, a.ProductId, date.ToUniversalTime(), a.Price, a.Count } }
                    );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
