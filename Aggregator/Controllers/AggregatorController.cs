using Aggregator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AggregatorController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IReviewService _reviewService;

        public AggregatorController(ICatalogService catalogService, IReviewService reviewService)
        {
            _catalogService = catalogService;
            _reviewService = reviewService;
        }

        [HttpGet("products-with-reviews")]
        public async Task<IActionResult> GetProductsWithReviews()
        {
            var products = await _catalogService.GetProductsAsync();

            foreach (var product in products)
            {
                var reviews = await _reviewService.GetReviewsAsync(product.id);
                product.reviews = reviews;
            }

            return Ok(products);
        }
    }
}
