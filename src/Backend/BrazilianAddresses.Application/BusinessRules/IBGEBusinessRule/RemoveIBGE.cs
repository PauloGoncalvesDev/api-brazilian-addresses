using AutoMapper;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;
using BrazilianAddresses.Application.Services.LoggedUser;
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

        private readonly ILoggedUser _loggedUser;

        public RemoveIBGE(IIBGEUpdateOnlyRepository ibgeUpdateOnlyRepository, IMapper mapper, IWorkUnit workUnit, ILoggedUser loggedUser)
        {
            _ibgeUpdateOnlyRepository = ibgeUpdateOnlyRepository;
            _mapper = mapper;
            _workUnit = workUnit;
            _loggedUser = loggedUser;
        }

        public async Task<IBGEResponseJson> Execute(string iBGECode)
        {
            IBGE ibgeToRemove = await _ibgeUpdateOnlyRepository.GetIBGEByIBGECodeToUpdate(iBGECode) ?? throw new ValidationException(APIMSG.NO_EXISTING_CODE);

            User user = await _loggedUser.GetLoggedUser();

            _mapper.Map(ibgeToRemove, ibgeToRemove);

            ibgeToRemove.UpdateUserId = user.Id.Value;

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
