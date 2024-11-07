using Domain.Enums;

namespace Domain.Entities
{
        public class Order
        {
            public int Id { get; set; }  
            public int CustomerId { get; set; }  
            public Customer Customer { get; set; } 

            public DateTime OrderDate { get; set; } = DateTime.Now; 
            public OrderStatus Status { get; set; }  
            public string ShippingAddress { get; set; }  
            public decimal TotalAmount { get; set; }  
        }
}