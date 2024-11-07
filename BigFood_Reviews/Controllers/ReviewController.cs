using BigFood_Reviews.Entities;
using BigFood_Reviews.Repositories;
using BigFood_Reviews.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BigFood_Reviews.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _unitOfWork.Reviews.GetAllAsync();
            return Ok(reviews);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _unitOfWork.Reviews.GetAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }



        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest();
            }

            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetById), new { id = review.Id }, review);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Review review)
        {
            if (review == null || id != review.Id)
            {
                return BadRequest();
            }

            var existingReview = await _unitOfWork.Reviews.GetAsync(id);
            if (existingReview == null)
            {
                return NotFound();
            }

            existingReview.Rating = review.Rating;
            existingReview.Comment = review.Comment;
            existingReview.CustomerId = review.CustomerId;

            await _unitOfWork.Reviews.ReplaceAsync(existingReview);
            await _unitOfWork.CommitAsync();
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _unitOfWork.Reviews.GetAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            await _unitOfWork.Reviews.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            return NoContent();
        }






        [HttpGet("reviews-with-customers")]
        public async Task<IActionResult> GetReviewsWithCustomers()
        {
            var reviewsWithCustomers = await _unitOfWork.Reviews.GetReviewsWithCustomersAsync();
            return Ok(reviewsWithCustomers);
        }





        [HttpGet("reviewsbyproductid/{productId}")] 
        public async Task<ActionResult<List<Review>>> GetAllReviewsByProductId(int productId) 
        { 
            var reviews = await _unitOfWork.Reviews.GetAllReviewsByProductIdAsync(productId); 
            return Ok(reviews); 
        }
    }

}


