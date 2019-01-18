namespace AddNServiceBus
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using NServiceBus;

    public static class AddNServiceBusServiceCollectionExtensions
    {
        public static IServiceCollection AddNServiceBus(this IServiceCollection services, string endpointName, Action<EndpointConfiguration> configuration)
        {
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            configuration(endpointConfiguration);
            return services.AddNServiceBus(endpointConfiguration);
        }

        public static IServiceCollection AddNServiceBus(this IServiceCollection services, EndpointConfiguration configuration)
        {
            // find out how to deal with shutdown timeout of 5 seconds
            var management = new SessionAndConfigurationHolder(configuration);
            services.AddSingleton<IMessageSession>(provider => management.Session);
            services.AddSingleton(management);
            services.AddHostedService<EndpointManagement>();
            return services;
        }
    }
}
