namespace EventBus.Abstractions
{
    public interface IIntegrationEvent
    {
        void Publish(string message);

        string Read();
    }
}