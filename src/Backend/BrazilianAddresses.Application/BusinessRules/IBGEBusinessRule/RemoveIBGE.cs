using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories;
using BrazilianAddresses.Domain.Repositories.IBGERepository;
using BrazilianAddresses.Exceptions.ExceptionsBase;
using BrazilianAddresses.Exceptions.ResourcesMessage;
using FluentValidation.Results;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule
{
    public class RemoveIBGE : IRemoveIBGE
    {
        private readonly IIBGEWriteOnlyRepository _ibgeWriteOnlyRepository;

        private readonly IIBGEReadOnlyRepository _ibgeReadOnlyRepository;

        private readonly IWorkUnit _workUnit;

        public RemoveIBGE(IIBGEWriteOnlyRepository ibgeWriteOnlyRepository, IIBGEReadOnlyRepository ibgeReadOnlyRepository, IWorkUnit workUnit)
        {
            _ibgeWriteOnlyRepository = ibgeWriteOnlyRepository;
            _ibgeReadOnlyRepository = ibgeReadOnlyRepository;
            _workUnit = workUnit;
        }

        public async Task<IBGEResponseJson> Execute(string iBGECode)
        {
            await ValidateIBGE(iBGECode);

            await _workUnit.Commit();

            return new IBGEResponseJson
            {
                Message = APIMSG.IBGE_CREATED,
                Success = true,
                IBGECode = iBGECode
            };
        }

        private async Task ValidateIBGE(string iBGECode)
        {
            ValidationResult validationResult = new ValidationResult();

            IBGE existingIBGE = await _ibgeReadOnlyRepository.GetIBGEByIBGECode(iBGECode);

            if (existingIBGE == null)
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(iBGECode, APIMSG.EXISTING_CODE));

            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationException(errorMessages);
            }
        }
    }
}
