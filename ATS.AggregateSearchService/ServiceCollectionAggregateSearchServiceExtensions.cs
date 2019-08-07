using ATS.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.AggregateSearchService
{
    public static class ServiceCollectionAggregateSearchServiceExtensions
    {
        public static IServiceCollection AddAggregateSearchService(this IServiceCollection services)
        {
            services.AddSingleton<AggregateSearchService>();
            services.AddTransient<IHostedService>(serviceProvider => serviceProvider.GetService<AggregateSearchService>());
            services.AddTransient<IAggregateSearchService>(serviceProvider=>serviceProvider.GetService<AggregateSearchService>());
            return services;
        }
    }
}
