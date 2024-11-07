using Aggregator.Models;

namespace Aggregator.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewModel>> GetReviewsAsync(int productId);
    }
}
