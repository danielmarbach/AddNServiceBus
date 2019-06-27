namespace Sample
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;

    internal class MyOtherHandler : IHandleMessages<MyOtherMessage>
    {
        public Task Handle(MyOtherMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine("Received other message");
            return Task.CompletedTask;
        }
    }
}