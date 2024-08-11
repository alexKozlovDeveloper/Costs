namespace CostKeeper.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
        public string[] Tags { get; set; }
        public float? Weight { get; set; }
        public float? EnergyValue { get; set; }        
        public float? Proteins { get; set; }        
        public float? Fats { get; set; }        
        public float? Carbohydrates { get; set; }
	}
}
