using Catalog_DAL.Entities;
using Catalog_DAL.Pagination.Parameters;
using Catalog_DAL.Pagination;
using Catalog_DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Catalog_DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(CatalogContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            var result = await table
                .Include(c => c.Category) 
                .ToListAsync();
            return result;
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            var result = await table
                .Where(c => c.Id == id)
                .Include(c => c.Category) 
                .SingleOrDefaultAsync();
            return result;
        }
    }
}

