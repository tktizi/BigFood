using BigFood_Reviews.Entities;

namespace BigFood_Reviews.Repositories.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task AddReviewAsync(Review review);

        Task<IEnumerable<Review>> GetReviewsWithCustomersAsync();

        Task<IEnumerable<Review>> GetAllReviewsByProductIdAsync(int productId);
    }
}
