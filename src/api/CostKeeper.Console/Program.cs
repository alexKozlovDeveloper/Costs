// See https://aka.ms/new-console-template for more information
using CostKeeper;
using CostKeeper.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var optionsBuilder = new DbContextOptionsBuilder<CostsDbContext>();
optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CostsDB;Username=postgres;Password=Admi$in1!");

var exportDate = DateTime.Now.Date.ToUniversalTime();

var startDate = exportDate.AddDays(-30);
var endDate = exportDate.AddDays(+30);

var folder = "Export";

if (!Directory.Exists(folder)) { Directory.CreateDirectory(folder); }

using (var dbContext = new CostsDbContext(optionsBuilder.Options))
{
	var checks = await dbContext.Checks
		.Where(a => a.Date >= startDate && a.Date <= endDate)
		.ToListAsync();

	var json = JsonSerializer.Serialize(checks, new JsonSerializerOptions { WriteIndented = true });

	var fileName = Path.Combine(folder, $"{exportDate:dd-MM-yyyy}.json");

	File.WriteAllText(fileName, json);	
}