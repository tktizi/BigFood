using BigFood_Reviews.Entities;

namespace BigFood_Reviews.Repositories.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
