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
    public class ListAllIBGEAddresses : IListAllIBGEAddresses
    {
        private readonly IMapper _mapper;

        private readonly IIBGEReadOnlyRepository _ibgeReadOnlyRepository;

        public ListAllIBGEAddresses(IIBGEReadOnlyRepository ibgeReadOnlyRepository, IMapper mapper)
        {
            _mapper = mapper;
            _ibgeReadOnlyRepository = ibgeReadOnlyRepository;
        }

        public async Task<List<AddressResponseJson>> Execute(PaginationBaseRequestJson pagination)
        {
            List<IBGE> ibges = await _ibgeReadOnlyRepository.GetAllIBGEAddress();

            List<IBGE> ibgesPaged = PaginatedList(ibges, pagination);

            List<AddressResponseJson> addressResponseJsons = _mapper.Map<List<AddressResponseJson>>(ibgesPaged);

            return addressResponseJsons;
        }

        private static List<IBGE> PaginatedList(List<IBGE> ibges, PaginationBaseRequestJson pagination)
        {
            ValidateIndexValue(ibges.Count, pagination);

            int jump = pagination.PageIndex * pagination.PageSize;

            return ibges.Skip(jump).Take(pagination.PageSize).ToList();
        }

        private static void ValidateIndexValue(int listLength, PaginationBaseRequestJson pagination)
        {
            int currentMaxIndexValue = listLength / pagination.PageSize;

            if (pagination.PageIndex > currentMaxIndexValue)
            {
                if (currentMaxIndexValue == 0)
                    throw new ValidationException(APIMSG.BLANK_PAGE);

                throw new ValidationException(string.Format(APIMSG.INDEX_TOO_LARGE, currentMaxIndexValue));
            }
        }
    }
}