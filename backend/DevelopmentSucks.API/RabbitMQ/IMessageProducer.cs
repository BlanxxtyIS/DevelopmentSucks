namespace DevelopmentSucks.API.RabbitMQ;

public interface IMessageProducer
{
    Task SendMessageAsync<T>(T message);
}
