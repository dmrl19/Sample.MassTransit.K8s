namespace Sample.MassTransit.K8s.Models.Configs;

public record RabbitMqConfig(string Uri, string Username, string Password);