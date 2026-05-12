namespace DoctorApi.RabbitQueue;

using Microsoft.AspNetCore.Mvc;
 

[ApiController]
[Route("api/[controller]")]
public class ProducerController : ControllerBase
{
    private readonly IRabbitMqProducer _producer;

    public ProducerController(IRabbitMqProducer producer)
    {
        _producer = producer;
    }

    [HttpPost]
    public async Task<IActionResult> PublishOrder(OrderMessage request)
    {
        request.OrderId = Guid.NewGuid();
        request.CreatedAt = DateTime.UtcNow;

        await _producer.PublishAsync(request);

        return Ok(new
        {
            Message = "Order published successfully",
            request.OrderId
        });
    }
}