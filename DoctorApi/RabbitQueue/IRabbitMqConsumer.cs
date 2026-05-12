namespace DoctorApi.RabbitQueue;

 

public interface IRabbitMqConsumer
{
    Task<AppointmentMessage?> ConsumeAsync(string queueName);
}