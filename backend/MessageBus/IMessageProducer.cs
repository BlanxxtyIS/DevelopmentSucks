namespace MessageBus;

public interface IMessageProducer
{
    Task SendMessageAsync<T>(T message);
}
