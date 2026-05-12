namespace DoctorApi.RabbitQueue;


public class RabbitMqSettings
{
    public string HostName { get; set; } = string.Empty;
    public int Port { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string VirtualHost { get; set; } = "/";
    public string QueueName { get; set; } = string.Empty;
}


public class OrderMessage
{
    public Guid OrderId { get; set; }
    public string Customer { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}

 