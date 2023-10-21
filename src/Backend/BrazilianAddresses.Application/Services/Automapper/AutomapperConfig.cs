using AutoMapper;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;

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

            MapperGETAddresses();

            MapperRemoveAddress();

            MapperCreateUser();
        }

        private void MapperIBGEUpdateRequest()
        {
            CreateMap<IBGEUpdateRequestJson, IBGE>()
                .ForMember(f => f.UpdateDate, opt => opt.MapFrom(m => DateTime.Now));
        }

        private void MapperGETAddresses()
        {
            CreateMap<IBGE, AddressResponseJson>()
                .ForMember(f => f.IBGECode, opt => opt.MapFrom(m => m.IBGECode))
                .ForMember(f => f.State, opt => opt.MapFrom(m => m.State))
                .ForMember(f => f.City, opt => opt.MapFrom(m => m.City));
        }

        private void MapperRemoveAddress()
        {
            CreateMap<IBGE, IBGE>()
                .ForMember(f => f.DeletionDate, opt => opt.MapFrom(m => DateTime.Now))
                .ForMember(f => f.UpdateDate, opt => opt.MapFrom(m => DateTime.Now));
        }

        private void MapperCreateUser()
        {
            CreateMap<UserRequestJson, User>()
                .ForMember(f => f.Email, opt => opt.MapFrom(m => m.Email))
                .ForMember(r => r.Password, config => config.Ignore())
                .ForMember(r => r.Salt, config => config.Ignore())
                .ForMember(f => f.Role, opt => opt.MapFrom(m => m.UserRole))
                .ForMember(f => f.CreationDate, opt => opt.MapFrom(m => DateTime.Now))
                .ForMember(f => f.UpdateDate, opt => opt.MapFrom(m => DateTime.Now));
        }
    }
}