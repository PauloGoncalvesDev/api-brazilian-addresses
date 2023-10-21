using AutoMapper;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using BrazilianAddresses.Exceptions.ExceptionsBase;
using BrazilianAddresses.Exceptions.ResourcesMessage;
using BrazilianAddresses.Domain.Repositories.IBGERepository;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;

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
            ValidateZeroIndex(pagination.PageIndex);

            List<IBGE> ibges = await _ibgeReadOnlyRepository.GetAllIBGEAddresses();

            List<IBGE> ibgesPaged = PaginatedList(ibges, pagination.PageIndex - 1, pagination.PageSize);

            if (ibgesPaged.Count == 0) throw new ValidationException(APIMSG.BLANK_PAGE);

            return _mapper.Map<List<AddressResponseJson>>(ibgesPaged);
        }

        public async Task<List<AddressResponseJson>> Execute(AddressesByStateRequestJson addressesByState)
        {
            ValidateZeroIndex(addressesByState.PageIndex);

            List<IBGE> ibges = await _ibgeReadOnlyRepository.GetIBGEAddressesByState(addressesByState.State.ToString());

            List<IBGE> ibgesPaged = PaginatedList(ibges, addressesByState.PageIndex - 1, addressesByState.PageSize);

            return _mapper.Map<List<AddressResponseJson>>(ibgesPaged);
        }

        public async Task<AddressResponseJson> Execute(CityAddressRequestJson addressByCity)
        {
            string city = _mapper.Map<CityAddressRequestJson>(addressByCity).City;

            IBGE cityAddress = await _ibgeReadOnlyRepository.GetIBGEAddressByCity(city) ?? throw new ValidationException(APIMSG.NO_EXISTING_CITY);

            return _mapper.Map<AddressResponseJson>(cityAddress);
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
                if (currentMaxIndexValue == 0) throw new ValidationException(APIMSG.BLANK_PAGE);

                throw new ValidationException(string.Format(APIMSG.INDEX_TOO_LARGE, currentMaxIndexValue));
            }
        }

        private static void ValidateZeroIndex(int index)
        {
            if (index == 0) throw new ValidationException(APIMSG.INVALID_PAGEINDEX);
        }
    }
}