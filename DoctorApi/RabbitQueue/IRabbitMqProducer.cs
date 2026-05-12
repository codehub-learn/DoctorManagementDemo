namespace DoctorApi.RabbitQueue;

 
 
public interface IRabbitMqProducer
{
    Task PublishAsync(AppointmentMessage message, string queueName);
}