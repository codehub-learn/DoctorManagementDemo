namespace DoctorApi.RabbitQueue;

 
 
public interface IRabbitMqProducer
{
    Task PublishAsync(OrderMessage message);
}