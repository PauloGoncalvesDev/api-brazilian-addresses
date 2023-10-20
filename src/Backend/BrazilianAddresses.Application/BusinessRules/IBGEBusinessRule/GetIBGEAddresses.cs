using AutoMapper;
using FluentValidation.Results;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using BrazilianAddresses.Exceptions.ExceptionsBase;
using BrazilianAddresses.Exceptions.ResourcesMessage;
using BrazilianAddresses.Domain.Repositories.IBGERepository;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;
using BrazilianAddresses.Application.Validators.IBGEValidator;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule
{
    public class GetIBGEAddresses : IGetIBGEAddresses
    {
        private readonly IMapper _mapper;

        private readonly IIBGEReadOnlyRepository _ibgeReadOnlyRepository;

        public GetIBGEAddresses(IIBGEReadOnlyRepository ibgeReadOnlyRepository, IMapper mapper)
        {
            _mapper = mapper;
            _ibgeReadOnlyRepository = ibgeReadOnlyRepository;
        }

        public async Task<List<AddressResponseJson>> Execute(PaginationBaseRequestJson pagination)
        {
            List<IBGE> ibges = await _ibgeReadOnlyRepository.GetAllIBGEAddresses();

            List<IBGE> ibgesPaged = PaginatedList(ibges, pagination.PageIndex, pagination.PageSize);

            List<AddressResponseJson> addressResponseJsons = _mapper.Map<List<AddressResponseJson>>(ibgesPaged);

            return addressResponseJsons;
        }

        public async Task<List<AddressResponseJson>> Execute(AddressesByStateRequestJson addressesByState)
        {
            ValidateAddress(addressesByState);

            _mapper.Map(addressesByState, addressesByState);

            List<IBGE> ibges = await _ibgeReadOnlyRepository.GetIBGEAddressesByState(addressesByState.State);

            List<IBGE> ibgesPaged = PaginatedList(ibges, addressesByState.PageIndex, addressesByState.PageSize);

            List<AddressResponseJson> addressResponseJsons = _mapper.Map<List<AddressResponseJson>>(ibgesPaged);

            return addressResponseJsons;
        }

        private static List<IBGE> PaginatedList(List<IBGE> ibges, int pageIndex, int pageSize)
        {
            ValidateIndexValue(ibges.Count, pageIndex, pageSize);

            int jump = pageIndex * pageSize;

            return ibges.Skip(jump).Take(pageSize).ToList();
        }

        private static void ValidateIndexValue(int listLength, int pageIndex, int pageSize)
        {
            int currentMaxIndexValue = listLength / pageSize;

            if (pageIndex > currentMaxIndexValue)
            {
                if (currentMaxIndexValue == 0)
                    throw new ValidationException(APIMSG.BLANK_PAGE);

                throw new ValidationException(string.Format(APIMSG.INDEX_TOO_LARGE, currentMaxIndexValue));
            }
        }

        public static void ValidateAddress(AddressesByStateRequestJson addressesByState)
        {
            ValidationResult validationResult = new ValidateAddress().Validate(addressesByState);

            if (!validationResult.IsValid)
            {
                List<string> errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ValidationException(errorMessages);
            }
        }
    }
}