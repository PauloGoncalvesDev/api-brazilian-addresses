using Microsoft.Extensions.DependencyInjection;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;

namespace BrazilianAddresses.Application
{
    public static class BuilderExtension
    {
        public static void AddApplication(this IServiceCollection serviceDescriptors)
        {
            AddApplicationIBGE(serviceDescriptors);
        }

        private static void AddApplicationIBGE(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<ICreateIBGE, CreateIBGE>();
            serviceDescriptors.AddScoped<IUpdateIBGE, UpdateIBGE>();
            serviceDescriptors.AddScoped<IGetIBGEAddresses, GetIBGEAddresses>();
            serviceDescriptors.AddScoped<IRemoveIBGE, RemoveIBGE>();
        }
    }
}