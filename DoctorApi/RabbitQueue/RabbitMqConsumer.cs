namespace DoctorApi.RabbitQueue;

using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
 

public sealed class RabbitMqConsumer : IRabbitMqConsumer, IDisposable
{
    private readonly RabbitMqSettings _settings;
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMqConsumer(IOptions<RabbitMqSettings> options)
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

        _connection = factory
            .CreateConnectionAsync()
            .GetAwaiter()
            .GetResult();

        _channel = _connection
            .CreateChannelAsync()
            .GetAwaiter()
            .GetResult();

        _channel.QueueDeclareAsync(
            queue: _settings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        ).GetAwaiter().GetResult();
    }
    //change the following to select a specific queue to consume from,
    //and return null if no message is available
    public async Task<OrderMessage?> ConsumeAsync()
    {
        /*
            BasicGetAsync:
            Pulls ONE message manually from queue.
        */

        var result = await _channel.BasicGetAsync(
            queue: _settings.QueueName,
            autoAck: false
        );

        if (result is null)
        {
            return null;
        }

        try
        {
            var body = result.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var message = JsonSerializer.Deserialize<OrderMessage>(json);

            /*
                ACK manually
                Removes message from queue
            */

            await _channel.BasicAckAsync(
                deliveryTag: result.DeliveryTag,
                multiple: false
            );

            return message;
        }
        catch
        {
            /*
                Return message back to queue
            */

            await _channel.BasicNackAsync(
                deliveryTag: result.DeliveryTag,
                multiple: false,
                requeue: true
            );

            throw;
        }
    }

    public void Dispose()
    {
        _channel?.Dispose();

        _connection?.Dispose();
    }
}