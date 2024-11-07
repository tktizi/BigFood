namespace Aggregator.Models
{
    public class ProductModel
    {
        public int id { get; set; }                       
        public string name { get; set; }                  
        public decimal price { get; set; }
        public IEnumerable<ReviewModel> reviews { get; internal set; }
    }
}
