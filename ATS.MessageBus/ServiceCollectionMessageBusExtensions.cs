using ATS.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.MessageBus
{
    public static class ServiceCollectionMessageBusExtensions
    {
        public static IServiceCollection AddMessageBusClient(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBusClient,MessageBusClient>();

            return services;
        }
    }
}
