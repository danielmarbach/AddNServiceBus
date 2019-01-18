namespace AddNServiceBus
{
    using NServiceBus;

    class SessionAndConfigurationHolder 
    {
        public SessionAndConfigurationHolder(EndpointConfiguration configuration) 
        {
            Configuration = configuration;
        }

        // Implement meaningful exception in getter
        public IMessageSession Session { get; set; }

        public EndpointConfiguration Configuration { get; }
    }
}
