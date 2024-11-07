using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
namespace Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IOrderDbContext _context;

        public CreateCustomerCommandHandler(IOrderDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Phone = command.Phone,
                Address = command.Address
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return customer.Id; 
        }
    }
}
