using FluentValidation;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Application.Validators.IBGEValidator.Default;

namespace BrazilianAddresses.Application.Validators.IBGEValidator
{
    public class ValidateAddress : AbstractValidator<AddressesByStateRequestJson>
    {
        public void ValidateState()
        {
            RuleFor(r => r.State).SetValidator(new ValidateStateDefault());
        }
    }
}