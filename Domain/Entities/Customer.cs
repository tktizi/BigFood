﻿
namespace Domain.Entities
{
        public class Customer
        {
            public int Id { get; set; }  
            public string FirstName { get; set; }  
            public string LastName { get; set; }  
            public string Email { get; set; } 
            public string Phone { get; set; }  
            public string Address { get; set; }  

           
            public IEnumerable<Order> Orders { get; set; }
        }
}
