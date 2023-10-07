using MassTransit;
using Sample.MassTransit.K8s.Models.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<RabbitMqConfig>().Bind(builder.Configuration.GetSection(nameof(RabbitMqConfig)));

var rabbitMqSettings = builder.Configuration.GetRequiredSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>();
builder.Services.AddMassTransit(mt => 
    mt.AddMassTransit((x =>
    {
        x.SetKebabCaseEndpointNameFormatter();
        x.SetInMemorySagaRepositoryProvider();
        
        x.UsingRabbitMq(((ctx, cfg) => 
        {
            cfg.Host(rabbitMqSettings!.Uri, c =>
            {
                c.Username(rabbitMqSettings.Username);
                c.Password(rabbitMqSettings.Password);
            });
            
            cfg.ConfigureEndpoints(ctx);
        }));
    })));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();