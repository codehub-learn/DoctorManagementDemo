namespace DoctorApi.RabbitQueue;

using Microsoft.AspNetCore.Mvc;
 

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IRabbitMqProducer _producer;

    public OrdersController(IRabbitMqProducer producer)
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