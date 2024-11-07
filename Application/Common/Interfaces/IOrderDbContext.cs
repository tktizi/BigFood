using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Common.Interfaces
{
    public interface IOrderDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
