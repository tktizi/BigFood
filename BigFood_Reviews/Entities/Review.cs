namespace BigFood_Reviews.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }

        public Customer Customer { get; set; }
    }

}
