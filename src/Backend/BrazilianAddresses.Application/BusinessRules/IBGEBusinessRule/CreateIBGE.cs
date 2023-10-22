using AutoMapper;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;
using BrazilianAddresses.Application.Services.LoggedUser;
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
    public class CreateIBGE : ICreateIBGE
    {
        private readonly IIBGEWriteOnlyRepository _ibgeWriteOnlyRepository;

        private readonly IIBGEReadOnlyRepository _ibgeReadOnlyRepository;

        private readonly IMapper _mapper;

        private readonly IWorkUnit _workUnit;

        private readonly ILoggedUser _loggedUser;

        public CreateIBGE(IIBGEWriteOnlyRepository ibgeWriteOnlyRepository, IMapper mapper, IWorkUnit workUnit, IIBGEReadOnlyRepository ibgeReadOnlyRepository, ILoggedUser loggedUser)
        {
            _ibgeWriteOnlyRepository = ibgeWriteOnlyRepository;
            _mapper = mapper;
            _workUnit = workUnit;
            _ibgeReadOnlyRepository = ibgeReadOnlyRepository;
            _loggedUser = loggedUser;
        }

        public async Task<IBGEResponseJson> Execute(IBGERequestJson ibgeRequestJson)
        {
            await ValidateIBGE(ibgeRequestJson);

            User user = await _loggedUser.GetLoggedUser();

            IBGE ibge = _mapper.Map<IBGE>(ibgeRequestJson);

            ibge.CreationUserId = user.Id.Value;
            ibge.UpdateUserId = user.Id.Value;

            await _ibgeWriteOnlyRepository.Add(ibge);

            await _workUnit.Commit();

            return new IBGEResponseJson
            {
                Message = APIMSG.IBGE_CREATED,
                Success = true,
                IBGECode = ibge.IBGECode
            };
        }

        private async Task ValidateIBGE(IBGERequestJson ibgeRequestJson)
        {
            ValidationResult validationResult = new ValidateIBGE().Validate(ibgeRequestJson);

            IBGE existingIBGE = await _ibgeReadOnlyRepository.GetIBGEByIBGECode(ibgeRequestJson.IBGECode);

            if (existingIBGE != null)
                validationResult.Errors.Add(new ValidationFailure(ibgeRequestJson.IBGECode, APIMSG.EXISTING_CODE));

            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ValidationException(errorMessages);
            }
        }
    }
}