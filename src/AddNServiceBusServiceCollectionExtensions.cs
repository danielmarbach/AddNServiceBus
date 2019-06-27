namespace AddNServiceBus
{
    using System;
    using System.Threading;
    using Microsoft.Extensions.DependencyInjection;
    using NServiceBus;

    public static class AddNServiceBusServiceCollectionExtensions
    {
        public static IServiceCollection AddNServiceBus(this IServiceCollection services, string endpointName, Action<EndpointConfiguration> configuration)
        {
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            configuration(endpointConfiguration);
            return services.AddNServiceBus(endpointConfiguration);
        }

        static IServiceCollection AddNServiceBus(this IServiceCollection services, EndpointConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddSingleton(new SessionAndConfigurationHolder(configuration));
            services.AddHostedService<NServiceBusService>();
            services.AddSingleton(provider =>
            {
                var management = provider.GetService<SessionAndConfigurationHolder>();
                if (management.MessageSession != null)
                {
                    return management.MessageSession;
                }

                var timeout = TimeSpan.FromSeconds(30);
                // SpinWait is here to accomodate for WebHost vs GenericHost difference
                // Closure here should be fine under the assumption we always fast track above once initialized
                if (!SpinWait.SpinUntil(() => management.MessageSession != null || management.StartupException != null,
                    timeout))
                {
                    throw new TimeoutException($"Unable to resolve the message session within '{timeout.ToString()}'.");
                }

                management.StartupException?.Throw();

                return management.MessageSession;
            });
            return services;
        }
    }
}
