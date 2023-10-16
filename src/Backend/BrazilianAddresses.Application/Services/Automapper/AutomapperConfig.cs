using AutoMapper;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Domain.Entities;

namespace BrazilianAddresses.Application.Services.Automapper
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<IBGERequestJson, IBGE>();
        }
    }
}
