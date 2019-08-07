using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ATS.Common;
using ATS.TuiProvider.DataContext;
using ATS.TuiProvider.Abstraction;
using Microsoft.Extensions.Hosting;

namespace ATS.TuiProvider.DI
{
    public static class ServiceCollectionTuiProviderExtensions
    {
        public static IServiceCollection AddTuiProvider(this IServiceCollection services)
        {
            services
                .AddSingleton<TuiSearchService>()
                .AddTransient<IHostedService>(serviceProvider => serviceProvider.GetService<TuiSearchService>())
                .AddSingleton<ITuiDataContext, TuiDataContext>()
                .AddTransient<ITuiTourRepository, TuiTourRepository>()
                .AddTransient<ITuiDictionariesRepository, TuiDictionariesRepository>();
            return services;
        }
    }
}
