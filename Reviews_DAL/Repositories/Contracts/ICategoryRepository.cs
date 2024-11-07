using Catalog_DAL.Entities;
using Catalog_DAL.Pagination;
using Catalog_DAL.Pagination.Parameters;
namespace Catalog_DAL.Repositories.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<PagedList<Category>> GetPaginatedCategories(CategoryParameters parameters);
    }
}
