using Application.Common;
using Application.DTO;
using MediatR;

namespace Application.Orders.Queries.GetCustomerOrders
{
    public class GetCustomerOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
        public int CustomerId { get; set; }
    }
}
