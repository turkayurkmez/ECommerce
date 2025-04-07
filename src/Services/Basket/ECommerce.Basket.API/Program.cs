using ECommerce.Basket.API.Services;
using ECommerce.Basket.Infrastructure.Extensions;
using ECommerce.Basket.Application.Extensions;
using ECommerce.MessageBroker.Extensions;
using ECommerce.Common.Extensions;
using ECommerce.Basket.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddMessageBroker(builder.Configuration,configureConsumers: config =>
{
    config.AddConsumer<ProductPriceChangedEventConsumer>(); 

});

var app = builder.Build();

app.UseExceptionHandling();

// Configure the HTTP request pipeline.
app.MapGrpcService<BasketGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");



app.Run();
