using MassTransit;

namespace Sample.MassTransit.K8s.Worker.Consumer.Consumers.NewProduct;

public class NewProductConsumerDefinition: ConsumerDefinition<NewProductConsumer>
{
    public NewProductConsumerDefinition()
    {
        EndpointName = "New-product";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<NewProductConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(x=>
        {
            x.Incremental(10, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
        });
        
        endpointConfigurator.DiscardSkippedMessages();
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    }
}