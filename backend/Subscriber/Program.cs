using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Starting subscriber...");

var factory = new ConnectionFactory
{
    HostName = "rabbitmq",
    UserName = "guest",
    Password = "guest"
};

using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync("orders", durable: true, exclusive: false, autoDelete: false);

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (sender, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Received message: {message}");

    // Здесь можно добавить обработку сообщения
    await Task.Delay(100); // Пример асинхронной операции
};

await channel.BasicConsumeAsync(queue: "orders", autoAck: true, consumer: consumer);

Console.WriteLine("Subscriber is running. Press Ctrl+C to exit.");

// Бесконечное ожидание (можно заменить на CancellationToken)
await Task.Delay(-1); // Или использовать ManualResetEvent