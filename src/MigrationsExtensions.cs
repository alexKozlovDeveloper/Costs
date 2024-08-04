using Microsoft.EntityFrameworkCore.Migrations;

namespace CostKeeper
{
    public static class MigrationsExtensions
    {
        public static void LoadFromJson<T>(
            MigrationBuilder migrationBuilder, 
            string tableName,
            string filePath,
            string[] labels,
            Func<T, object[,]> func
            )
        {
            T[]? items = FileExtensions.LoadFromJsonFile<T[]>(filePath);

            foreach (var item in items)
            {
                migrationBuilder.InsertData(
                    table: tableName,
                    columns: labels,
                    values: func(item)
                    );
            }
        }
    }
}
