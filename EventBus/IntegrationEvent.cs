using System;
using EventBus.Abstractions;

namespace EventBus
{
    public class IntegrationEvent : IIntegrationEvent, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Publish(string message)
        {
            // var factory = new ConnectionFactory
            // {

            // };
        }
    }
}