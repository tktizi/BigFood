using Catalog_BLL.DTO.Requests;
using Catalog_BLL.DTO.Responses;
using Catalog_DAL.Entities;

namespace Catalog_BLL.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse> GetProductByIdAsync(int productId);
        Task<ProductResponse> AddProductAsync(ProductRequest productRequest);
        Task<ProductResponse> UpdateProductAsync(ProductRequest productRequest);
        Task DeleteProductAsync(int id);
    }
}
