using ATS.OtherProvider.Abstraction;
using ATS.OtherProvider.DataContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.OtherProvider.DI
{
    public static class ServiceCollectionOtherProviderExtensions
    {
        public static IServiceCollection AddOtherProvider(this IServiceCollection services)
        {
            services
                .AddSingleton<OtherSearchService>()
                .AddTransient<IHostedService>(serviceProvider => serviceProvider.GetService<OtherSearchService>())
                .AddSingleton<IOtherDataContext, OtherDataContext>()
                .AddTransient<IOtherTourRepository, OtherTourRepository>()
                .AddTransient<IOtherDictionariesRepository, OtherDictionariesRepository>();

            return services;
        }
    }
}
