using MassTransit;
using Sample.MassTransit.K8s.Models.Configs;
using Sample.MassTransit.K8s.Worker.Consumer;
using Sample.MassTransit.K8s.Worker.Consumer.Consumers.NewProduct;
using Sample.MassTransit.K8s.Worker.Consumer.Consumers.NewUser;

var builder = Host.CreateDefaultBuilder(args);

var host = builder.ConfigureServices((ctx, services) =>
{
    services.AddHostedService<Worker>();
    
    var rabbitMqSettings = ctx.Configuration.GetRequiredSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>();
    services.AddMassTransit(mt =>
        mt.AddMassTransit((x =>
        {
            x.AddConsumer<NewUserConsumer, NewUserConsumerDefinition>();
            x.AddConsumer<NewProductConsumer, NewProductConsumerDefinition>();
            
            x.SetKebabCaseEndpointNameFormatter();
            x.SetInMemorySagaRepositoryProvider();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(rabbitMqSettings!.Uri, c =>
                {
                    c.Username(rabbitMqSettings.Username);
                    c.Password(rabbitMqSettings.Password);
                });

                cfg.ConfigureEndpoints(ctx);
            });
        })));
}).Build();

host.Run();