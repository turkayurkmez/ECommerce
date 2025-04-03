using ECommerce.Catalog.Infrastructure.Extensions;
using ECommerce.Catalog.Application.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);




var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        var response = new
        {
            StatusCode = 500,
            Message = exception?.Message ?? "Bir hata oluştu",
            Path = exceptionHandlerPathFeature?.Path
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();
