using Orleans.Runtime;
using Orleans.Services;

namespace Standalone.Silo.Services.Generators;

public interface IBackgroundGeneratorGrain : IGrainService
{
}

public class BackgroundGeneratorGrain : GrainService, IBackgroundGeneratorGrain
{
    private readonly ILogger<BackgroundGeneratorGrain> _logger;

    public BackgroundGeneratorGrain(
        IServiceProvider services,
        GrainId id,
        Orleans.Runtime.Silo silo,
        ILoggerFactory loggerFactory,
        IGrainFactory grainFactory)
        : base(id, silo, loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<BackgroundGeneratorGrain>();
    }

    protected override async Task StartInBackground()
    {
        await base.StartInBackground();
        _logger.LogWarning("Starting timer");

        RegisterTimer(Tick, null,
            TimeSpan.FromSeconds(5),
            TimeSpan.FromSeconds(5)
        );
    }

    private Task Tick(object arg)
    {
        _logger.LogWarning("Tick");
        return Task.CompletedTask;
    }
}