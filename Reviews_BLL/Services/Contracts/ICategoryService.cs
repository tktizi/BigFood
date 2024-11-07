using Catalog_BLL.DTO.Requests;
using Catalog_BLL.DTO.Responses;
using Catalog_DAL.Entities;

namespace Catalog_BLL.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<ShortCategoryResponse>> GetAllCategoriesAsync();
        Task<CategoryResponse> GetCategoryByIdAsync(int categoryId);
        Task<CategoryResponse> AddCategoryAsync(CategoryRequest categoryRequest);
        Task<CategoryResponse> UpdateCategoryAsync(CategoryRequest categoryRequest);
        Task DeleteCategoryAsync(int id);
    }
}
