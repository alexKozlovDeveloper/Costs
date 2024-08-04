namespace CostKeeper.Models
{
    public class Product
    {
        public string Id { get; set; }

        public string? Description { get; set; }
        public string Category { get; set; }
        public string[] Tags { get; set; }
        public int Weight { get; set; }
        public int CaloriesPer100g { get; set; }        
    }
}
