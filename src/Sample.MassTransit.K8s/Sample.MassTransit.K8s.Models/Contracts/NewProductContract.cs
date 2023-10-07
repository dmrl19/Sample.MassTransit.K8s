namespace Sample.MassTransit.K8s.Models.Contracts;

public record NewProductContract(Guid Id, string Name, int Quantity);