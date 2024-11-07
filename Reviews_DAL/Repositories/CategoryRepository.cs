using Catalog_DAL.Entities;
using Catalog_DAL.Pagination;
using Catalog_DAL.Pagination.Parameters;
using Catalog_DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Catalog_DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Category>> GetAllAsync()
        {
            var result = await table
                .Include(c => c.Products) 
                .ToListAsync();
            return result;
        }

        public override async Task<Category> GetByIdAsync(int id)
        {
            var result = await table
                .Where(c => c.Id == id)
                .Include(c => c.Products) 
                .SingleOrDefaultAsync();
            return result;
        }

        public async Task<PagedList<Category>> GetPaginatedCategories(CategoryParameters parameters)
        {
            return await PagedList<Category>.ToPagedListAsync(table.AsQueryable(), parameters.PageNumber, parameters.PageSize);
        }
    }
}
