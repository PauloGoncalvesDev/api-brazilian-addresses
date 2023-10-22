using BrazilianAddresses.Exceptions.ResourcesMessage;
using FluentValidation;

namespace BrazilianAddresses.Application.Validators.IBGEValidator.Default
{
    public class ValidateCodeDefault : AbstractValidator<string>
    {
        public ValidateCodeDefault()
        {
            RuleFor(ibgeCode => ibgeCode).NotEmpty().WithMessage(APIMSG.EMPTY_IBGECODE);
            When(ibgeCode => !string.IsNullOrEmpty(ibgeCode), () =>
            {
                RuleFor(ibgeCode => ibgeCode.Length).Equal(7).WithMessage(APIMSG.INVALID_CODE);
            });
        }
    }
}
