using CostKeeper.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostKeeper.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var files = Directory.GetFiles("Data\\Products");

			foreach (var file in files)
			{
				MigrationsExtensions.LoadFromJson<Product>(
					migrationBuilder,
					"Products",
					file,
					["Id", "Description", "Category", "Tags", "Weight", "EnergyValue", "Proteins", "Fats", "Carbohydrates"],
					a => new object[,] { { a.Id, a.Description, a.Category, a.Tags, a.Weight, a.EnergyValue, a.Proteins, a.Fats, a.Carbohydrates } }
					);
			}
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
