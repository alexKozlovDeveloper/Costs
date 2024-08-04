using CostKeeper;
using CostKeeper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;

Console.WriteLine("Starting...");

var d = DateTime.UtcNow.ToString("o");


string filePath = "Logs\\Products.json";

// Чтение содержимого файла
string jsonString = await File.ReadAllTextAsync(filePath);

// Десериализация JSON в массив объектов
Product[] items = JsonSerializer.Deserialize<Product[]>(jsonString);

string filePath2 = "Logs\\AugustChecks.json";

// Чтение содержимого файла
string jsonString2 = await File.ReadAllTextAsync(filePath2);

// Десериализация JSON в массив объектов
Check[] items2 = JsonSerializer.Deserialize<Check[]>(jsonString2);

var d2 = 3;

//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json")
//    .Build();

//// Получаем строку подключения из конфигурации
//var connectionString = configuration.GetConnectionString("DefaultConnection");

//// Настраиваем контекст базы данных для PostgreSQL
//var optionsBuilder = new DbContextOptionsBuilder<CostsDbContext>();
//optionsBuilder.UseNpgsql(connectionString);

//using (var context = new CostsDbContext(optionsBuilder.Options))
//{

//}

