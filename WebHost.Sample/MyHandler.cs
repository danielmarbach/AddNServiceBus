namespace Sample
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;

    internal class MyHandler : IHandleMessages<MyMessage>
    {
        public Task Handle(MyMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine("Received message");
            return Task.CompletedTask;
        }
    }
}