using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Vibe.Chat.Hubs;
using Vibe.Configurator.Configuration;
using Vibe.EF;
using Vibe.EF.Interface;
using Vibe.Services.Clients;
using Vibe.Services.Clients.Interface;
using Vibe.Services.Clients.Repositories;
using Vibe.Services.Employees;
using Vibe.Services.Employees.Interface;
using Vibe.Services.Employees.Repositories;
using Vibe.Services.Infrastructure;
using Vibe.Services.Infrastructure.Interface;
using Vibe.Services.Infrastructure.Repositories;
using Vibe.Services.Rents;
using Vibe.Services.Rents.Interface;
using Vibe.Services.Rents.Repositories;
using Vibe.Services.Scooters;
using Vibe.Services.Scooters.Interface;
using Vibe.Services.Scooters.Repositories;
using Vibe.Services.SupportRequests;
using Vibe.Services.SupportRequests.Interface;
using Vibe.Services.SupportRequests.Repositories;
using Vibe.Tools;
using Vibe.Tools.JWT;

var builder = WebApplication.CreateBuilder(args);

String? launchProfile = Environment.GetEnvironmentVariable("LAUNCH_PROFILE");
switch (launchProfile)
{
    case "DOTNETASPIRE":
        builder.AddServiceDefaults();
        builder.AddRedisDistributedCache("cache");
        break;
    case "HTTPS":
    case "HTTP":
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            var connection = builder.Configuration.GetConnectionString("Redis");
            options.Configuration = connection;
        });
        break;
    case "DOCKER_COMPOSE":
        // Вариант с Docker Compose
        var redisConnectionString = builder.Configuration.GetSection("Redis:ConnectionString").Value;
        builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
        break;
    default:
        throw new InvalidOperationException($"Не указан профиль запуска");
}

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen(c =>
{
    // Определение безопасности
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Введите токен JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // Используйте "bearer" для JWT
        BearerFormat = "JWT"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    // Требование безопасности
    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };
    c.AddSecurityRequirement(securityRequirement);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:8081", "http://localhost:7221", "https://localhost:5173", "http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

#region Services
builder.Services.AddScoped<IScootersService, ScootersService>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton<IScootersProvider, ScootersProvider>();
builder.Services.AddHttpClient<IScootersProvider, ScootersProvider>();

builder.Services.AddScoped<ISupportRequestService, SupportRequestService>();
#endregion

#region Repositories
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRentRepository, RentRepository>();
builder.Services.AddScoped<IPhoneCodeRepository, PhoneCodeRepository>();
builder.Services.AddScoped<IScooterRepository, ScooterRepository>();
builder.Services.AddScoped<ISupportRequestRepository, SupportRequestRepository>();
#endregion

builder.Services.AddDbContext<DataContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseLowerCaseNamingConvention()
);

JWTSettings.SecretKey = CustomConfigurationExtensions.GetRequiredStringValue(builder.Configuration, nameof(JWTSettings), nameof(JWTSettings.SecretKey));
JWTSettings.Issuer = CustomConfigurationExtensions.GetRequiredStringValue(builder.Configuration, nameof(JWTSettings), nameof(JWTSettings.Issuer));
JWTSettings.Audience = CustomConfigurationExtensions.GetRequiredStringValue(builder.Configuration, nameof(JWTSettings), nameof(JWTSettings.Audience));
SymmetricSecurityKey signingKey = JWTTools.FormSingingKey(JWTSettings.SecretKey!);

VirtualScooterSettings.Host = CustomConfigurationExtensions.GetRequiredStringValue(builder.Configuration, nameof(VirtualScooterSettings), nameof(VirtualScooterSettings.Host));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = JWTSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = JWTSettings.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingKey,
        ValidateLifetime = true
    };
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});

// Configure the HTTP request pipeline.

app.UseCors("AllowSpecificOrigins");
app.MapHub<ChatHub>("/SupportRequestChat");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
