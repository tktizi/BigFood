namespace Aggregator.Models
{
    public class ReviewModel
    {
        public int id { get; set; }
        public int customerid { get; set; }// Ідентифікатор користувача, який залишив відгук
        public int productid { get; set; }    // Ідентифікатор продукту, на який залишено відгук
        public int rating { get; set; }       // Оцінка від 1 до 5
        public string comment { get; set; }    // Текст відгуку
    }
}
