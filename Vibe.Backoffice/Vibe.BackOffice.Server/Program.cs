using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Vibe.BackOffice.Server.Tools;
using Vibe.EF;
using Vibe.EF.Entities;
using Vibe.EF.Interface;
using Vibe.Services.Clients;
using Vibe.Services.Clients.Interface;
using Vibe.Services.Scooters;
using Vibe.Services.Scooters.Interface;
using Vibe.Tools.JWT;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Services
builder.Services.AddScoped<IScootersService, ScootersService>();
builder.Services.AddScoped<IClientService, ClientService>();


builder.Services.AddSingleton<IScootersProvider, ScootersProvider>();
builder.Services.AddHttpClient<IScootersProvider, ScootersProvider>();
#endregion

#region Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IPhoneCodeRepository, PhoneCodeRepository>();
builder.Services.AddScoped<IDataRepository<ScooterEntity>, ScooterRepository>();
#endregion

builder.Services.AddDbContext<DataContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseLowerCaseNamingConvention()
);

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection(nameof(JWTSettings)));

String? secretKey = builder.Configuration.GetSection($"{nameof(JWTSettings)}:{nameof(JWTSettings.SecretKey)}").Value;
var jwtSettings = new
{
    Issuer = builder.Configuration.GetSection($"{nameof(JWTSettings)}:{nameof(JWTSettings.Issuer)}").Value,
    Audience = builder.Configuration.GetSection($"{nameof(JWTSettings)}:{nameof(JWTSettings.Audience)}").Value,
    SigningKey = JWTTools.FormSingingKey(secretKey!)
};

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
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = jwtSettings.SigningKey,
        ValidateLifetime = true
    };
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseCors(configure => configure.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
