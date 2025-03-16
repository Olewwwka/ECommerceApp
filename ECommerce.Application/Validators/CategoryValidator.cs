using ECommerce.Application.Contracts.Requests;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Название продукта обязательно")
              .Length(3, 50).WithMessage("Название должно содержать 3-50 символов");

            RuleFor(x => x.Description)
                .Length(5, 100).WithMessage("Описание должно содержать 5-100 символов");
        }
    }
}
