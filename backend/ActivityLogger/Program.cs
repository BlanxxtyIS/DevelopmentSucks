// See https://aka.ms/new-console-template for more information
using ActivityLogger;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;


Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory() { HostName = "localhost" };
var connection = await factory.CreateConnectionAsync(); // не await, просто CreateConnection()
var channel = await connection.CreateChannelAsync();      // CreateModel, не CreateChannelAsync


await channel.QueueDeclareAsync(
    queue: "user-activity", 
    durable: false, 
    exclusive: false,
    autoDelete: false, 
    arguments: null
);

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var json = Encoding.UTF8.GetString(body);
    var message = JsonSerializer.Deserialize<UserActivity>(json);

    if (message is not null)
    {
        using var db = new AppDbContext();
        db.Activities.Add(message);
        await db.SaveChangesAsync();

        Console.WriteLine($"[+] Activity saved: {message.Action}");
    }
};

await channel.BasicConsumeAsync(
    queue: "user-activity", 
    autoAck: true, 
    consumer: consumer
);

Console.WriteLine("Waiting for activity messages...");
await Task.Delay(-1);