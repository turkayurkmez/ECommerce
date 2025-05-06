using ECommerce.Common.Extensions;
using ECommerce.EventBus.Extensions;
using ECommerce.Identity.API.Endpoints;
using ECommerce.Identity.Application.Extensions;
using ECommerce.Identity.Infrastructure.Extensions;
using ECommerce.Identity.Infrastructure.Persistence;
using ECommerce.Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddIdentityInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//JWT Authentication
var jwtSettings = new JwtTokenSettings();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSettings.Issuer,
//        ValidAudience = jwtSettings.Audience,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
//        ClockSkew = TimeSpan.Zero // Remove delay of token expiration
//    };
//});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<IdentityDbContext>();
            context.Database.EnsureCreated();
            // İleride migration işlemleri için DbInitializer sınıfı oluşturulabilir
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while creating the database.");
        }
    }
}

app.UseExceptionHandling();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapGroup("/api/v1/auth")
   .MapAuthEndpoints()
   .WithTags("Auth")
   .WithOpenApi();

// User endpoints
app.MapGroup("/api/v1/users")
   .MapUserEndpoints()
   .WithTags("Users")
   .WithOpenApi();

// Role endpoints
app.MapGroup("/api/v1/roles")
   .MapRoleEndpoints()
   .WithTags("Roles")
   .WithOpenApi();



app.Run();

