namespace Sample.MassTransit.K8s.Models.Contracts;

public record NewUserContract(Guid Id, string Name, DateTime Date);