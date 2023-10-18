using BrazilianAddresses.Application.Validators.IBGEValidator.Default;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Domain.Enum;
using BrazilianAddresses.Exceptions.ResourcesMessage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrazilianAddresses.Application.Validators.IBGEValidator
{
    public class ValidateUpdateIBGE : AbstractValidator<IBGEUpdateRequestJson>
    {
        public ValidateUpdateIBGE()
        {
            ValidateCode();

            ValidateState();

            ValidateCity();
        }

        private void ValidateCode()
        {
            RuleFor(r => r.IBGECode).SetValidator(new ValidateCodeDefault());
        }

        private void ValidateState()
        {
            RuleFor(r => r.State).SetValidator(new ValidateStateDefault());
        }

        private void ValidateCity()
        {
            RuleFor(r => r.City).SetValidator(new ValidateCityDefault());
        }
    }
}
