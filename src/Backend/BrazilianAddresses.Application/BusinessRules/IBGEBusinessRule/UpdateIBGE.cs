using AutoMapper;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;
using BrazilianAddresses.Application.Validators.IBGEValidator;
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
    public class UpdateIBGE : IUpdateIBGE
    {
        private readonly IIBGEUpdateOnlyRepository _ibgeUpdateOnlyRepository;

        private readonly IMapper _mapper;

        private readonly IWorkUnit _workUnit;

        public UpdateIBGE(IIBGEUpdateOnlyRepository ibgeUpdateOnlyRepository,  IMapper mapper, IWorkUnit workUnit)
        {
            _ibgeUpdateOnlyRepository = ibgeUpdateOnlyRepository;
            _mapper = mapper;
            _workUnit = workUnit;
        }

        public async Task<IBGEResponseJson> Execute(IBGERequestJson ibgeRequestJson)
        {
            await ValidateIBGE(ibgeRequestJson);
        }

        public async Task ValidateIBGE(IBGERequestJson ibgeRequestJson)
        {
            ValidationResult validationResult = new ValidateIBGE().Validate(ibgeRequestJson);

            IBGE existingIBGE = await _ibgeUpdateOnlyRepository.GetIBGEByIBGECodeToUpdate(ibgeRequestJson.IBGECode);

            if(existingIBGE == null)
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(ibgeRequestJson.IBGECode, APIMSG.NO_EXISTING_CODE));

            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationException(errorMessages);
            }
        }
    }
}
