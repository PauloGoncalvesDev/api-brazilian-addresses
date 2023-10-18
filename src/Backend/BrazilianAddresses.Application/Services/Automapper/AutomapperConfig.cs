using AutoMapper;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Domain.Entities;

namespace BrazilianAddresses.Application.Services.Automapper
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<IBGERequestJson, IBGE>()
                .ForMember(f => f.CreationDate, opt => opt.MapFrom(m => DateTime.Now))
                .ForMember(f => f.UpdateDate, opt => opt.MapFrom(m => DateTime.Now));

            MapperIBGEUpdateRequest();
        }

        private void MapperIBGEUpdateRequest()
        {
            CreateMap<IBGEUpdateRequestJson, IBGE>()
                .ForMember(f => f.UpdateDate, opt => opt.MapFrom(m => DateTime.Now));
        }
    }
}
