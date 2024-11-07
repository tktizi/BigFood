using Application.Common.Interfaces;
using Application.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerDto>>
    {
        private readonly IOrderDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IOrderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
           
           var customers = await _context.Customers.ToListAsync();
            var result = _mapper.Map<List<CustomerDto>>(customers);
            return await Task.Run(() => result);
        }
    }
}
