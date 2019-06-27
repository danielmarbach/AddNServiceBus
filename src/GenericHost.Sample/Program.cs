namespace GenericHost.Sample
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using AddNServiceBus;
    using NServiceBus;

    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddNServiceBus("MyEndpoint", endpointConfiguration =>
                    {
                        endpointConfiguration.UseTransport<LearningTransport>();
                    });

                    services.AddHostedService<MyBackgroundService>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}
