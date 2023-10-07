using MassTransit;
using Sample.MassTransit.K8s.Models.Contracts;

namespace Sample.MassTransit.K8s.Wroker.CronJob.Producer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBus _bus;

    public Worker(IBus bus, ILogger<Worker> logger)
    {
        _logger = logger;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        await _bus.Publish(new NewProductContract(Guid.NewGuid(), "Product-Name-FromCronJob", new Random().Next(10)), stoppingToken);
        Environment.Exit(0);
    }
}