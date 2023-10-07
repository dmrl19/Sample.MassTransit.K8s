using MassTransit;
using Sample.MassTransit.K8s.Models.Contracts;

namespace Sample.MassTransit.K8s.Worker.Consumer.Consumers.NewProduct;

public class NewProductConsumer: IConsumer<NewProductContract>
{
    private readonly ILogger<NewProductConsumer> _logger;

    public NewProductConsumer(ILogger<NewProductConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<NewProductContract> context)
    {
        if (context.Message.Quantity % 2 == 0)
        {
            _logger.LogError("Product creation failed with count {RetryCount} at {DateTime}", context.GetRetryCount(), DateTime.UtcNow);
            throw new Exception("Fail to create product");
        }
        
        _logger.LogInformation("Created Product: {Name} and quantity: {Quantity}", context.Message.Name, context.Message.Quantity);
        return Task.CompletedTask;
    }
}