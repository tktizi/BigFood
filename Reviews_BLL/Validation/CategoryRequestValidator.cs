using Catalog_BLL.DTO.Requests;
using FluentValidation;

namespace Catalog_BLL.Validation
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .Length(3, 50).WithMessage("Category name must be between 3 and 50 characters.");
        }
    }
}
