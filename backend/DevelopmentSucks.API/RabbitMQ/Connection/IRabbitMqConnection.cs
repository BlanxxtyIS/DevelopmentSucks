using RabbitMQ.Client;

namespace DevelopmentSucks.API.RabbitMQ.Connection;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
    Task InitializeAsync();
}
