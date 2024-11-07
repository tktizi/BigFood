using Aggregator.Models;

namespace Aggregator.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductModel>> GetProductsAsync();
    }
}
