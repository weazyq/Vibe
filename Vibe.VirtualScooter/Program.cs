using Vibe.VirtualScooter.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

Double latitude = Double.Parse(Environment.GetEnvironmentVariable("SCOOTER_LATITUDE")!);
Double longitude = Double.Parse(Environment.GetEnvironmentVariable("SCOOTER_LONGITUDE")!);

VirtualScooter.Instance.SetCoordinates(latitude, longitude);

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
