using BrazilianAddresses.Exceptions.ResourcesMessage;
using FluentValidation;

namespace BrazilianAddresses.Application.Validators.UserValidator
{
    public class ValidatePasswordDefault : AbstractValidator<string>
    {
        private readonly int MinPasswordSize = 6;

        public ValidatePasswordDefault()
        {
            RuleFor(password => password).NotEmpty().WithMessage(string.Format(APIMSG.EMPTY_PASSWORD));
            When(password => !string.IsNullOrEmpty(password), () =>
            {
                RuleFor(password => password.Length).GreaterThanOrEqualTo(MinPasswordSize).WithMessage(string.Format(APIMSG.LENGTH_PASSWORD, MinPasswordSize));
            });
        }
    }
}
