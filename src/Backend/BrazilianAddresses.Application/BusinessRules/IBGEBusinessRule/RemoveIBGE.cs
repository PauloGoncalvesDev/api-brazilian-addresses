using AutoMapper;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;
using BrazilianAddresses.Communication.Responses;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories;
using BrazilianAddresses.Domain.Repositories.IBGERepository;
using BrazilianAddresses.Exceptions.ExceptionsBase;
using BrazilianAddresses.Exceptions.ResourcesMessage;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule
{
    public class RemoveIBGE : IRemoveIBGE
    {
        private readonly IIBGEUpdateOnlyRepository _ibgeUpdateOnlyRepository;

        private readonly IMapper _mapper;

        private readonly IWorkUnit _workUnit;

        public RemoveIBGE(IIBGEUpdateOnlyRepository ibgeUpdateOnlyRepository, IMapper mapper, IWorkUnit workUnit)
        {
            _ibgeUpdateOnlyRepository = ibgeUpdateOnlyRepository;
            _mapper = mapper;
            _workUnit = workUnit;
        }

        public async Task<IBGEResponseJson> Execute(string iBGECode)
        {
            IBGE ibgeToRemove = await _ibgeUpdateOnlyRepository.GetIBGEByIBGECodeToUpdate(iBGECode) ?? throw new ValidationException(APIMSG.NO_EXISTING_CODE);

            _mapper.Map(ibgeToRemove, ibgeToRemove);

            _ibgeUpdateOnlyRepository.Update(ibgeToRemove);

            await _workUnit.Commit();

            return new IBGEResponseJson
            {
                Message = APIMSG.IBGE_REMOVED,
                Success = true,
                IBGECode = iBGECode
            };
        }
    }
}
