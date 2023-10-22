﻿using BrazilianAddresses.Domain.Extension;
using BrazilianAddresses.Domain.Repositories;
using BrazilianAddresses.Domain.Repositories.IBGERepository;
using BrazilianAddresses.Domain.Repositories.UserRepository;
using BrazilianAddresses.Infrastructure.RepositoryAccess;
using BrazilianAddresses.Infrastructure.RepositoryAccess.Repository;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BrazilianAddresses.Infrastructure
{
    public static class Bootstrapper
    {
        public static void AddRepository(this IServiceCollection services, IConfiguration configurationManager)
        {
            AddFluentMigrator(services, configurationManager);

            AddContext(services, configurationManager);
            AddWorkUnit(services);
            AddRepositories(services);
        }

        private static void AddContext(IServiceCollection services, IConfiguration configurationManager)
        {
            var connectionString = configurationManager.GetFullConnection();

            services.AddDbContext<BrazilianAddressesContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddWorkUnit(IServiceCollection services)
        {
            services.AddScoped<IWorkUnit, WorkUnit>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>()
                    .AddScoped<IUserReadOnlyRepository, UserRepository>()
                    .AddScoped<IIBGEReadOnlyRepository, IBGERepository>()
                    .AddScoped<IIBGEWriteOnlyRepository, IBGERepository>()
                    .AddScoped<IIBGEUpdateOnlyRepository, IBGERepository>()
                    .AddScoped<IIBGERemoveOnlyRepository, IBGERepository>();
        }

        private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager)
        {
            services.AddFluentMigratorCore().ConfigureRunner(c =>
            c.AddSqlServer()
            .WithGlobalConnectionString(configurationManager.GetFullConnection())
            .ScanIn(Assembly.Load("BrazilianAddresses.Infrastructure"))
            .For.All());
        }
    }
}
