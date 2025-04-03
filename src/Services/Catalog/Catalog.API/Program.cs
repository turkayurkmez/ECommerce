using ECommerce.Catalog.Application.Extensions;
using ECommerce.Catalog.Infrastructure.Extensions;
using ECommerce.Catalog.Infrastructure.Persistence;
using ECommerce.Common.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);


//eğer geliştirme ortamındaysa



var app = builder.Build();

//eğer geliştirme ortamındaysa:
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
       var initializer = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        if (!initializer.Database.EnsureCreated())
        {
            await CatalogDbInitializer.MigrateAsync(app.Services, app.Logger);
        }
      
       // await initializer.SeedAsync();
    }

}
app.UseExceptionHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();
