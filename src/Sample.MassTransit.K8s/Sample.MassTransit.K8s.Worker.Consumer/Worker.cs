using MassTransit;
using Sample.MassTransit.K8s.Models.Contracts;

namespace Sample.MassTransit.K8s.Worker.Consumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBus _bus;
    
    public Worker(ILogger<Worker> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await _bus.Publish(new NewUserContract(Guid.NewGuid(), "User-Name-FromWorker", DateTime.UtcNow), stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
