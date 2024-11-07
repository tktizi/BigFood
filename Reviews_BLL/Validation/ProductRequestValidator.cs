using Catalog_BLL.DTO.Requests;
using FluentValidation;

namespace Catalog_BLL.Validation
{
    public class ProductRequestValidator : AbstractValidator<ProductRequest>
    {
        public ProductRequestValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(3, 100).WithMessage("Product name must be between 3 and 100 characters.");

            RuleFor(product => product.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(product => product.CategoryId)
                .NotNull().WithMessage("Category Id is required.")
                .GreaterThan(0).WithMessage("Category Id must be greater than 0.");
        }
    }
}
