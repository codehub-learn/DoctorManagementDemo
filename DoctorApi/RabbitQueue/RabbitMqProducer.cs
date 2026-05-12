namespace DoctorApi.RabbitQueue;

using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
 

public sealed class RabbitMqProducer : IRabbitMqProducer, IDisposable
{
    private readonly RabbitMqSettings _settings;

    private readonly IConnection _connection;

    private readonly IChannel _channel;

    public RabbitMqProducer(IOptions<RabbitMqSettings> options)
    {
        _settings = options.Value;

        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password,
            VirtualHost = _settings.VirtualHost
        };

        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();

        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

        _channel.QueueDeclareAsync(
            queue: _settings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        ).GetAwaiter().GetResult();
    }

    public async Task PublishAsync(OrderMessage message)
    {
        var json = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties
        {
            Persistent = true
        };

        await _channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: _settings.QueueName,
            mandatory: false,
            basicProperties: properties,
            body: body
        );
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}