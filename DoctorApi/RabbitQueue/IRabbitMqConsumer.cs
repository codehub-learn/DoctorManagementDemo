namespace DoctorApi.RabbitQueue;

 

public interface IRabbitMqConsumer
{
    Task<OrderMessage?> ConsumeAsync();
}