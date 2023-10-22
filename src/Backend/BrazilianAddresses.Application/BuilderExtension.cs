﻿using Microsoft.Extensions.DependencyInjection;
using BrazilianAddresses.Application.Services.Cryptography;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule;
using BrazilianAddresses.Application.BusinessRules.UserBusinessRule;
using BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces;
using BrazilianAddresses.Application.BusinessRules.UserBusinessRule.Interfaces;
using Microsoft.Extensions.Configuration;
using BrazilianAddresses.Application.Services.Token;
using BrazilianAddresses.Application.Services.LoggedUser;

namespace BrazilianAddresses.Application
{
    public static class BuilderExtension
    {
        public static void AddApplication(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            AddApplicationIBGE(serviceDescriptors);

            AddApplicationUser(serviceDescriptors);

            AddApplicationLogin(serviceDescriptors);

            AddApplicationServicePasswordEncryption(serviceDescriptors);

            AddApplicationServiceTokenJwt(serviceDescriptors, configuration);

            AddApplicationLoggedUser(serviceDescriptors);
        }

        private static void AddApplicationIBGE(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<ICreateIBGE, CreateIBGE>();
            serviceDescriptors.AddScoped<IUpdateIBGE, UpdateIBGE>();
            serviceDescriptors.AddScoped<IGetIBGEAddresses, GetIBGEAddresses>();
            serviceDescriptors.AddScoped<IRemoveIBGE, RemoveIBGE>();
        }

        private static void AddApplicationUser(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<ICreateUser, CreateUser>();
        }

        private static void AddApplicationLogin(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<ILoginUser, LoginUser>();
        }

        private static void AddApplicationServicePasswordEncryption(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped(options => new PasswordEncryption());
        }

        private static void AddApplicationServiceTokenJwt(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            int expirationTime = Convert.ToInt32(configuration.GetRequiredSection("Configuration:Jwt:JwtExpirationTime").Value);

            string securityPassword = configuration.GetRequiredSection("Configuration:Jwt:JwtSecurityPassword").Value;

            serviceDescriptors.AddScoped(options => new TokenController(expirationTime, securityPassword));
        }

        private static void AddApplicationLoggedUser(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<ILoggedUser, LoggedUser>();
        }
    }
}