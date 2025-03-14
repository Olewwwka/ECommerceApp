using ECommerce.Application.Contracts.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;

namespace ECommerce.Application.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Логин обязателен")
            .MinimumLength(3).WithMessage("Логин должен содержать минимум 3 символа")
            .MaximumLength(50).WithMessage("Логин не должен превышать 50 символов");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль обязателен")
                .MinimumLength(6).WithMessage("Пароль должен содержать минимум 6 символов");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email обязателен")
                .EmailAddress().WithMessage("Некорректный формат email");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Имя обязательно")
                .MaximumLength(50).WithMessage("Имя не должно превышать 50 символов");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Фамилия обязательна")
                .MaximumLength(50).WithMessage("Фамилия не должна превышать 50 символов");
        }
    }
}
