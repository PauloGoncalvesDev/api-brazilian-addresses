﻿using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Domain.Enum;
using BrazilianAddresses.Exceptions.ResourcesMessage;
using FluentValidation;

namespace BrazilianAddresses.Application.Validators.IBGEValidator
{
    public class ValidateIBGE : AbstractValidator<IBGERequestJson>
    {
        public ValidateIBGE()
        {
            ValidateCode();

            ValidateState();

            ValidateCity();
        }

        private void ValidateCode()
        {
            RuleFor(r => r.IBGECode).NotEmpty().WithMessage(APIMSG.EMPTY_IBGECODE);
            When(r => !string.IsNullOrEmpty(r.IBGECode), () =>
            {
                RuleFor(r => r.IBGECode.Length).Equal(7).WithMessage(APIMSG.INVALID_CODE);
            });
        }

        private void ValidateState()
        {
            RuleFor(r => r.State).
                NotEmpty().WithMessage(APIMSG.EMPTY_STATE)                
                .Must(state => StateInEnum(state?.Replace(" ", string.Empty))).WithMessage(APIMSG.INVALID_STATE);
        }

        private void ValidateCity()
        {
            RuleFor(r => r.City).NotEmpty().WithMessage(APIMSG.EMPTY_CITY)
            .Must(NotContainNumber).WithMessage(APIMSG.INVALID_CITY);
        }

        private bool StateInEnum(string state)
        {  
            return Enum.TryParse<StateEnum>(state, out _);
        }

        private bool NotContainNumber(string input)
        {
            return input != null && !input.Any(char.IsDigit);
        }
    }
}
