using System.Text.Json;

namespace CostKeeper
{
    public static class FileExtensions
    {
        public static T? LoadFromJsonFile<T>(string filePath) 
        {
            string jsonString = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}
