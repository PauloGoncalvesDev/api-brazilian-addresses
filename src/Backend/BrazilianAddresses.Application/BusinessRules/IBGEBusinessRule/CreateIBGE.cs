using AutoMapper;
using BrazilianAddresses.Application.Validators.IBGEValidator;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories;
using BrazilianAddresses.Domain.Repositories.IBGERepository;
using BrazilianAddresses.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule
{
    public class CreateIBGE : ICreateIBGE
    {
        private readonly IIBGEWriteOnlyRepository _ibgeWriteOnlyRepository;

        private readonly Mapper _mapper;

        private readonly IWorkUnit _workUnit;

        public CreateIBGE(IIBGEWriteOnlyRepository ibgeWriteOnlyRepository, Mapper mapper, IWorkUnit workUnit)
        {
            _ibgeWriteOnlyRepository = ibgeWriteOnlyRepository;
            _mapper = mapper;
            _workUnit = workUnit;
        }

        public async Task Execute(IBGERequestJson ibgeRequestJson)
        {
            ValidateIBGE(ibgeRequestJson);

            IBGE ibge = _mapper.Map<IBGE>(ibgeRequestJson);

            await _ibgeWriteOnlyRepository.Add(ibge);

            await _workUnit.Commit();
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
