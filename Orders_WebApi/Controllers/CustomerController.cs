using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Queries.GetCustomers;
using Application.Orders.Commands.CreateOrder;
using Application.Orders.Queries.GetCustomerOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Orders_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
        {
            var Id = await _mediator.Send(command);
            return Ok(Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _mediator.Send(new GetCustomersQuery());
            return Ok(customers);
        }
    }
}
