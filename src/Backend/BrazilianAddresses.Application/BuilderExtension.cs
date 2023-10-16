using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule;
using Microsoft.Extensions.DependencyInjection;

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
        }
    }
}
