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
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private IProductService _productService;
        private IValidator<ProductRequest> _validator;

        public ProductController(IValidator<ProductRequest> validator, ILogger<ProductController> logger, IProductService productService)
        {
            _validator = validator;
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts()
        {
            var result = await _productService.GetAllProductsAsync();
            return Ok(result);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById([FromRoute] int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ProductResponse>> AddProduct([FromBody] ProductRequest productRequest)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(productRequest);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return BadRequest(validationResult.Errors);
            }

            return await _productService.AddProductAsync(productRequest);
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct([FromRoute] int id, [FromBody] ProductRequest productRequest)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(productRequest);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return BadRequest(validationResult.Errors);
            }

            var result = await _productService.UpdateProductAsync(productRequest);
            return Ok(result);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
