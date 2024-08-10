namespace CostKeeper.Models
{
    public class Check
    {
        public int Id { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime Date { get; set; }        
        public float Price { get; set; }
        public float Count { get; set; }
    }
}
