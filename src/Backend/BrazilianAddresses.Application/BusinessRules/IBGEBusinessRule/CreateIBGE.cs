using BrazilianAddresses.Application.Validators.IBGEValidator;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule
{
    public class CreateIBGE : ICreateIBGE
    {
        public async Task Execute(IBGERequestJson ibgeRequestJson)
        {
            ValidateIBGE(ibgeRequestJson);
        }

        private static void ValidateIBGE(IBGERequestJson ibgeRequestJson)
        {
            ValidationResult validationResult = new ValidateIBGE().Validate(ibgeRequestJson);

            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationException(errorMessages);
            }
        }
    }
}
