using Orleans.Runtime;

namespace Standalone.Silo.Services.Generators;

public class BackgroundGeneratorGrain : GrainService, IBackgroundGeneratorGrain
{
    private readonly IGrainFactory _grainFactory;
    private readonly ILogger<BackgroundGeneratorGrain> _logger;
    private readonly IClusterClient _client;

    public BackgroundGeneratorGrain(
        IServiceProvider services,
        GrainId id,
        Orleans.Runtime.Silo silo,
        ILoggerFactory loggerFactory,
        IGrainFactory grainFactory, IClusterClient client)
        : base(id, silo, loggerFactory)
    {
        _grainFactory = grainFactory;
        _client = client;
        _logger = loggerFactory.CreateLogger<BackgroundGeneratorGrain>();
    }

    protected override async Task StartInBackground()
    {
        await base.StartInBackground();
        _logger.LogWarning("Starting timer");

        RegisterTimer(Tick, "channel-1",
            TimeSpan.FromSeconds(5),
            TimeSpan.FromSeconds(5)
        );
    }

    private async Task Tick(object arg)
    {
        var grain = _grainFactory.GetGrain<IStreamProducerGrain>(arg as string);
        await grain.Produce(DateTime.UtcNow.ToString("O"));
    }
}