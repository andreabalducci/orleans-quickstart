using Orleans.Runtime;
using Standalone.Silo.Shared;

namespace Standalone.Silo.Services.Generators;

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