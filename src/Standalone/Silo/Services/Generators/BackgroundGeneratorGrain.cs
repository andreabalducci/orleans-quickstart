using Orleans.Runtime;
using Orleans.Services;
using Standalone.Silo.Shared;

namespace Standalone.Silo.Services.Generators;

public interface IBackgroundGeneratorGrain : IGrainService
{
}

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

public interface IStreamProducerGrain : IGrainWithStringKey
{
    ValueTask Produce(string value);
}

public class StreamProducerGrain : Grain, IStreamProducerGrain
{
    private readonly ILogger<StreamProducerGrain> _logger;

    public StreamProducerGrain(ILogger<StreamProducerGrain> logger)
    {
        _logger = logger;
    }

    public async ValueTask Produce(string value)
    {
        _logger.LogInformation("Produce {Value}", value);
       
        var provider = this.GetStreamProvider(Constants.STREAM_PROVIDER_NAME);
        var streamId = StreamId.Create("RANDOMDATA", this.GetPrimaryKeyString());
        var stream = provider.GetStream<int>(streamId);
        await stream.OnNextAsync(DateTime.Now.Millisecond);
    }
}