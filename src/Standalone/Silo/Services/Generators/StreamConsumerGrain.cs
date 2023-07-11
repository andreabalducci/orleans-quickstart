using Orleans.Runtime;
using Orleans.Streams;
using Standalone.Silo.Shared;

namespace Standalone.Silo.Services.Generators;

public interface IStreamConsumerGrain : IGrainWithStringKey
{
    ValueTask StartReceive();
    ValueTask StopReceive();
}

public class StreamConsumerGrain : Grain, IStreamConsumerGrain, IAsyncObserver<string>
{
    private StreamSubscriptionHandle<string>? _subscription;
    private readonly ILogger<StreamConsumerGrain> _logger;

    public StreamConsumerGrain(ILogger<StreamConsumerGrain> logger)
    {
        _logger = logger;
    }

    public async ValueTask StartReceive()
    {
        _logger.LogInformation("Start receiving stream");
        var provider = this.GetStreamProvider(Constants.STREAM_PROVIDER_NAME);
        var streamId = StreamId.Create(Constants.STREAM_NAMESPACE, this.GetPrimaryKeyString());

        var stream = provider.GetStream<string>(streamId);
        this._subscription = await stream.SubscribeAsync(this);
    }

    public async ValueTask StopReceive()
    {
        _logger.LogInformation("Stop receiving stream");
        if (_subscription != null)
        {
            await _subscription.UnsubscribeAsync();
            _subscription = null;
        }
    }

    public Task OnNextAsync(string item, StreamSequenceToken? token = null)
    {
        _logger.LogInformation("Received {Item} from stream", item);
        return Task.CompletedTask;
    }

    public Task OnCompletedAsync()
    {
        _logger.LogInformation("Stream completed");
        return Task.CompletedTask;
    }

    public Task OnErrorAsync(Exception ex)
    {
        _logger.LogError(ex, "Stream error");
        return Task.CompletedTask;
    }
}