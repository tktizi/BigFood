using Application.DTO;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Queries.GetCustomers
{
    public class GetCustomersQuery : IRequest<IEnumerable <CustomerDto>>
    {
        
    }
}
