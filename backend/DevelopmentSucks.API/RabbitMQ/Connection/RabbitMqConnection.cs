using RabbitMQ.Client;

namespace DevelopmentSucks.API.RabbitMQ.Connection;

public class RabbitMqConnection : IRabbitMqConnection, IDisposable
{
    private IConnection? _connection;

    public IConnection Connection => _connection ?? throw new InvalidOperationException("RabbitMQ connection is not initialized.");

    public async Task InitializeAsync()
    {
        var factory = new ConnectionFactory
        {
            HostName = "rabbitmq",
            UserName = "guest",
            Password = "guest"
        };

        _connection = await factory.CreateConnectionAsync();
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }
}
