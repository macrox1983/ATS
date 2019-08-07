using ATS.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.AggregateDictionaryService
{
    public static class ServiceCollectionDictionariesServiceExtensions
    {
        public static IServiceCollection AddAggregateDictionariesService(this IServiceCollection services)
        {
            services.AddSingleton<AggregateDictionariesService>();
            services.AddTransient<IHostedService>(serviceProvider => serviceProvider.GetService<AggregateDictionariesService>());
            services.AddTransient<IAggregateDictionariesService>(serviceProvider => serviceProvider.GetService<AggregateDictionariesService>());
            return services;
        }
    }
}
