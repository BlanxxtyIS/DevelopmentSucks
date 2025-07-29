using MessageBus.Connection;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace MessageBus;

public class RabbitMqProducer : IMessageProducer
{
    private readonly IRabbitMqConnection _connection;

    public RabbitMqProducer(IRabbitMqConnection connection)
    {
        _connection = connection;
    }

    public async Task SendMessageAsync<T>(T message)
    {
        var channel = await _connection.Connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "orders", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(exchange: "", routingKey: "orders", body: body);

        await channel.CloseAsync();

        channel.Dispose();
    }

}
