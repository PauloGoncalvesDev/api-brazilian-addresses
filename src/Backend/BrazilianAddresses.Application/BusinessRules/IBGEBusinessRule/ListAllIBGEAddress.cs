using AutoMapper;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;
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

                List<IBGE> ibgesPaged = ibges.GetRange(pagination.PageIndex * pagination.PageSize, pagination.PageSize);

                List<IBGEResponseJson> de = _mapper.Map<List<IBGEResponseJson>>(ibgesPaged);

                await _workUnit.Commit();

                return de;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}