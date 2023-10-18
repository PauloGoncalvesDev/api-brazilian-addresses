using BrazilianAddresses.Exceptions.ResourcesMessage;
using FluentValidation;

namespace BrazilianAddresses.Application.Validators.IBGEValidator.Default
{
    public class ValidateCityDefault : AbstractValidator<string>
    {
        public ValidateCityDefault()
        {
            RuleFor(city => city).NotEmpty().WithMessage(APIMSG.EMPTY_CITY)
            .Must(NotContainNumber).WithMessage(APIMSG.INVALID_CITY);
        }

        private bool NotContainNumber(string input)
        {
            return input != null && !input.Any(char.IsDigit);
        }
    }
}
