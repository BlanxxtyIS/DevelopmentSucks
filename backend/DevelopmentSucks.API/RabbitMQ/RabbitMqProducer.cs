using DevelopmentSucks.API.RabbitMQ.Connection;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DevelopmentSucks.API.RabbitMQ;

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

        await channel.QueueDeclareAsync(queue: "orders", exclusive: false, autoDelete: false, arguments: null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        Console.WriteLine($"Sending to RabbitMQ: {json}");

        await channel.BasicPublishAsync(exchange: "", routingKey: "orders", body: body);

        await channel.CloseAsync();

        channel.Dispose();
    }
}
