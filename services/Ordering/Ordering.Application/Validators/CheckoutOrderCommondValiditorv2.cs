
using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class CheckoutOrderCommondValiditorv2:AbstractValidator<checkoutOrderCommondV2>
    {
        public CheckoutOrderCommondValiditorv2()
        {
            RuleFor(o => o.UserName)
                .NotEmpty()
                .WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(70)
                .WithMessage("{UserName} must not exceed 70 characters");

            RuleFor(o => o.TotalPrice)
                .NotEmpty()
                .WithMessage("{TotalPrice} is required")
                .NotNull()
                .GreaterThan(-1)
                .WithMessage("{TotalPrice} should not be -ve");

        }
    }
}
