using AutoMapper;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
using BrazilianAddresses.Exceptions.ExceptionsBase;
using BrazilianAddresses.Exceptions.ResourcesMessage;
using BrazilianAddresses.Domain.Repositories.IBGERepository;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule
{
    public class ListAllIBGEAddress : IListAllIBGEAddress
    {
        private readonly IWorkUnit _workUnit;

        private readonly IMapper _mapper;

        private readonly IIBGEReadOnlyRepository _ibgeReadOnlyRepository;

        public ListAllIBGEAddress(IIBGEReadOnlyRepository ibgeReadOnlyRepository, IWorkUnit workUnit, IMapper mapper)
        {
            _mapper = mapper;
            _workUnit = workUnit;
            _ibgeReadOnlyRepository = ibgeReadOnlyRepository;
        }

        public async Task<List<IBGEResponseJson>> Execute(PaginationBaseRequestJson pagination)
        {
            try
            {
                List<IBGE> ibges = await _ibgeReadOnlyRepository.GetAllIBGEAddress();

                List<IBGE> ibgesPaged = PaginatedList(ibges, pagination);

                List<IBGEResponseJson> ibgeResponseJsons = _mapper.Map<List<IBGEResponseJson>>(ibgesPaged);

                await _workUnit.Commit();

                return ibgeResponseJsons;
            }
            catch (ValidationException ex)
            {
                throw ex;
            }
        }

        private static List<IBGE> PaginatedList(List<IBGE> ibges, PaginationBaseRequestJson pagination)
        {
            int currentMaxIndexValue = ibges.Count / pagination.PageSize;

            if (pagination.PageIndex > currentMaxIndexValue)
                throw new ValidationException(string.Format(APIMSG.INDEX_TOO_LARGE, currentMaxIndexValue));

            int jump = pagination.PageIndex * pagination.PageSize;

            return ibges.Skip(jump).Take(pagination.PageSize).ToList();
        }
    }
}