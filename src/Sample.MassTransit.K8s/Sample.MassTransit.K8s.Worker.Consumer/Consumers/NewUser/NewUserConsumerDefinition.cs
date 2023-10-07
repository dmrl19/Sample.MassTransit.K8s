using MassTransit;

namespace Sample.MassTransit.K8s.Worker.Consumer.Consumers.NewUser;

public class NewUserConsumerDefinition: ConsumerDefinition<NewUserConsumer>
{
    public NewUserConsumerDefinition()
    {
        EndpointName = "New-user";
    }
}