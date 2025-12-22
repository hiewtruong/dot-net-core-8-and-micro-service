using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validations;

public class CheckoutCommandValidatorV2 : AbstractValidator<CheckoutOrderCommandV2>
{
    public CheckoutCommandValidatorV2()
    {
        RuleFor(o => o.UserName)
            .NotEmpty()
            .WithMessage("{UserName} is required.")
            .NotNull()
            .MaximumLength(70)
            .WithMessage("{UserName} must not exceed 70 characters.");
        RuleFor(o => o.TotalPrice)
            .NotEmpty()
            .WithMessage("{TotalPrice} is required.")
            .GreaterThan(-1)
            .WithMessage("{TotalPrice} should not be -ve");
    }
}