using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IOrderDbContext _context;

        public CreateOrderCommandHandler(IOrderDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.UtcNow,
                ShippingAddress = request.ShippingAddress,
                TotalAmount = request.TotalAmount
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync(cancellationToken);
;            return order.Id;
        }
    }
}
