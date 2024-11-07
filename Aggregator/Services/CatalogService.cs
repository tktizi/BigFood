using Aggregator.Models;
using Aggregator.Services.Interfaces;
using System.Text.Json;

namespace Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("/api/Product/GetAllProducts");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(content);
            return result;
        }
    }
}
