
namespace Application.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
