using BrazilianAddresses.Domain.Enum;
using BrazilianAddresses.Exceptions.ResourcesMessage;
using FluentValidation;
using System.Linq;

namespace BrazilianAddresses.Application.Validators.IBGEValidator.Default
{
    public class ValidateStateDefault : AbstractValidator<string>
    {
        public ValidateStateDefault()
        {
            RuleFor(state => state)
                .NotEmpty().WithMessage(APIMSG.EMPTY_STATE)
                .Must(state => StateInEnum(state?.Replace(" ", string.Empty))).WithMessage(APIMSG.INVALID_STATE);
        }

        private bool StateInEnum(string state)
        {
            return Enum.TryParse<StateEnum>(state, out _);
        }
    }
}
