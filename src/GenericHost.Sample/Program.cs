namespace Sample
{
    using System.Threading.Tasks;
    using AddNServiceBus;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NServiceBus;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddNServiceBus("MyEndpoint",
                        endpointConfiguration => { endpointConfiguration.UseTransport<LearningTransport>(); });

                    services.AddHostedService<MyBackgroundService>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}