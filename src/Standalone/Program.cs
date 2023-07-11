using Orleans.Providers;
using Standalone.Silo.Services;
using Standalone.Silo.Services.Generators;
using Standalone.Silo.Services.Storage;
using Standalone.Silo.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseOrleans(siloBuilder =>
    {
        siloBuilder.UseLocalhostClustering();
        siloBuilder.UseDashboard();
        siloBuilder.AddMemoryStreams(Constants.STREAM_PROVIDER_NAME);
        siloBuilder.AddMemoryGrainStorage(ProviderConstants.DEFAULT_PUBSUB_PROVIDER_NAME);

        siloBuilder.ConfigureServices(s =>
        {
            s.AddGrainService<LocalDiskService>()
                .AddGrainService<BackgroundGeneratorGrain>()
                .AddSingleton<ILocalDiskServiceClient, LocalDiskServiceClient>();
        });
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .UseConsoleLifetime();

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