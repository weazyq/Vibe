var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");
builder.AddProject<Projects.Vibe_BackOffice_Server>("vibe-backoffice-server").WithReference(redis).WithEnvironment("LAUNCH_PROFILE", "DOTNETASPIRE");
builder.AddProject<Projects.Vibe_VirtualScooter>("vibe-virtual-scooter");

builder.Build().Run();
