using MassTransit;
using Sample.MassTransit.K8s.Models.Contracts;

namespace Sample.MassTransit.K8s.Worker.Consumer.Consumers.NewUser;

public class NewUserConsumer: IConsumer<NewUserContract>
{
    private readonly ILogger<NewUserConsumer> _logger;
    
    public NewUserConsumer(ILogger<NewUserConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<NewUserContract> context)
    {
        _logger.LogInformation("Created User: {Name}", context.Message.Name);
        return Task.CompletedTask;
    }
}