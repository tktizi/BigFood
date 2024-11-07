using Aggregator.Models;
using Aggregator.Services.Interfaces;
using System.Text.Json;

namespace Aggregator.Services
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _httpClient;

        public ReviewService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ReviewModel>> GetReviewsAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/Review/reviewsbyproductid/{productId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ReviewModel>>(content);
        }
    }
}
