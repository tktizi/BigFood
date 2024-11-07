using Application.Customers.Commands.CreateCustomer;
using Application.DTO;
using Application.Orders.Commands.CreateOrder;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<CreateOrderCommand, Order>().ReverseMap();
            CreateMap<CreateCustomerCommand, Customer>().ReverseMap();
        }
    }
}
