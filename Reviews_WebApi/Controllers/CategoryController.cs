using Catalog_BLL.DTO.Requests;
using Catalog_BLL.DTO.Responses;
using Catalog_BLL.Services.Contracts;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private ICategoryService _categoryService;
        private IValidator<CategoryRequest> _validator;

        public CategoryController(IValidator<CategoryRequest> validator, ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _validator = validator;
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategoryById([FromRoute] int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("CreateCategory")]
        public async Task<ActionResult<CategoryResponse>> AddCategory([FromBody] CategoryRequest categoryRequest)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(categoryRequest);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return BadRequest(validationResult.Errors);
            }

            return await _categoryService.AddCategoryAsync(categoryRequest);
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<ActionResult<CategoryResponse>> UpdateCategory([FromRoute] int id, [FromBody] CategoryRequest categoryRequest)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(categoryRequest);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return BadRequest(validationResult.Errors);
            }

            var result = await _categoryService.UpdateCategoryAsync(categoryRequest);
            return Ok(result);
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
