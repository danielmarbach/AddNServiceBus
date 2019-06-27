namespace GenericHost.Sample
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using NServiceBus;

    class MyBackgroundService : BackgroundService
    {
        private readonly Lazy<IMessageSession> sessionProvider;

        public MyBackgroundService(Lazy<IMessageSession> sessionProvider)
        {
            this.sessionProvider = sessionProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var session = sessionProvider.Value;
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                await session.SendLocal(new MyMessage());
            }
        }
    }
}