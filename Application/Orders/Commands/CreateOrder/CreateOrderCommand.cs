using Application.Common;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
    }

}
