using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sample.MassTransit.K8s.Models.Configs;
using Sample.MassTransit.K8s.Models.Contracts;

namespace Sample.MassTransit.K8s.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController: ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly RabbitMqConfig _rabbitMqConfig;
    
    public UsersController(IPublishEndpoint publishEndpoint, IOptions<RabbitMqConfig> rabbitMqConfig)
    {
        _publishEndpoint = publishEndpoint;
        _rabbitMqConfig = rabbitMqConfig.Value;
    }

    [HttpPost(Name = "SendMessage")]
    public async Task SendMessage()
    {
        await _publishEndpoint.Publish(new NewUserContract(Guid.NewGuid(),"User-Name-FromApi", DateTime.UtcNow));
    }
    
    [HttpGet(Name = "PrintEnvVariables")]
    public Task<RabbitMqConfig> PrintEnvVariables()
    {
        return Task.FromResult(_rabbitMqConfig);
    }
}