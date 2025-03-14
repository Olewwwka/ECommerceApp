using ECommerce.Application.Contracts.Requests;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class AddProductValidator : AbstractValidator<AddProductRequest>
    {
        public AddProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название продукта обязательно")
                .Length(3, 50).WithMessage("Название должно содержать 3-50 символов");

            RuleFor(x => x.Description)
                .Length(5,100).WithMessage("Описание должно содержать 5-100 символов");

            RuleFor(x => x.Price)
                 .GreaterThan(0).WithMessage("Цена должна быть больше 0");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Количество на складе не может быть отрицательным");

            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Категория обязательна");
        }
    }
}
