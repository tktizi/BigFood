using Application.Common.Interfaces;
using Application.DTO;
using AutoMapper;
using MediatR;

namespace Application.Orders.Queries.GetCustomerOrders
{
    public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerOrdersQueryHandler(IOrderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
      
        public async Task<IEnumerable<OrderDto>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = _context.Orders.Where(x => x.CustomerId == request.CustomerId);
            var result = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return await Task.Run(() => result);
        }
    }
}
