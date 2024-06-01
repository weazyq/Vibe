using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Vibe.Chat.Hubs;
using Vibe.Configurator.Configuration;
using Vibe.EF;
using Vibe.EF.Interface;
using Vibe.Services;
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

builder.AddServiceDefaults();
builder.AddRedisDistributedCache("cache");
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:8081", "http://localhost:7221", "https://localhost:5173")
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

// Configure the HTTP request pipeline.

app.UseCors("AllowSpecificOrigins");
app.MapHub<ChatHub>("/SupportRequestChat");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
