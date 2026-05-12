namespace DoctorApi.RabbitQueue;

using Microsoft.AspNetCore.Mvc;
 
[ApiController]
[Route("api/[controller]")]
public class ConsumerController : ControllerBase
{
    private readonly IRabbitMqConsumer _consumer;

    public ConsumerController(IRabbitMqConsumer consumer)
    {
        _consumer = consumer;
    }

    [HttpGet("consume/{queueName}")]
    public async Task<IActionResult> Consume([FromRoute] string queueName)
    {
        var message = await _consumer.ConsumeAsync(queueName);
        if (message is null)
        {
            return NotFound(new
            {
                Message = "No messages in queue"
            });
        }
        return Ok(message);
    }



}