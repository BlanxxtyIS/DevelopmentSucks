using RabbitMQ.Client;

namespace MessageBus.Connection;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}
