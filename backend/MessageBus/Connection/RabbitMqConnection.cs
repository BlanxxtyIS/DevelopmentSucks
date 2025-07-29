using RabbitMQ.Client;

namespace MessageBus.Connection;

public class RabbitMqConnection: IRabbitMqConnection, IDisposable
{
    private IConnection? _connection;
    public IConnection Connection => _connection!;

    private async Task ConnectAsync()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
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
