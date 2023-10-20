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

        public UpdateIBGE(IIBGEUpdateOnlyRepository ibgeUpdateOnlyRepository, IMapper mapper, IWorkUnit workUnit)
        {
            _ibgeUpdateOnlyRepository = ibgeUpdateOnlyRepository;
            _mapper = mapper;
            _workUnit = workUnit;
        }

        public async Task<IBGEResponseJson> Execute(IBGEUpdateRequestJson ibgeUpdateRequestJson)
        {
            IBGE ibge = await _ibgeUpdateOnlyRepository.GetIBGEByIBGECodeToUpdate(ibgeUpdateRequestJson.IBGECode);

            await ValidateIBGE(ibgeUpdateRequestJson, ibge);

            _mapper.Map(ibgeUpdateRequestJson, ibge);

            _ibgeUpdateOnlyRepository.Update(ibge);

            await _workUnit.Commit();

            return new IBGEResponseJson
            {
                Message = APIMSG.UPDATED_IBGE,
                Success = true,
                IBGECode = ibge.IBGECode
            };
        }

        public async Task ValidateIBGE(IBGEUpdateRequestJson ibgeUpdateRequestJson, IBGE ibge)
        {
            ValidationResult validationResult = new ValidateUpdateIBGE().Validate(ibgeUpdateRequestJson);       

            if(ibge == null)
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(ibgeUpdateRequestJson.IBGECode, APIMSG.NO_EXISTING_CODE));

            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationException(errorMessages);
            }
        }
    }
}
