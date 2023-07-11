using Microsoft.AspNetCore.Mvc;
using Orleans.Runtime;
using Orleans.Streams;
using Standalone.Silo.Services.Generators;
using Standalone.Silo.Shared;

namespace Standalone.Controllers;

[ApiController]
[Route("[controller]")]
public class StreamController
{
    private readonly IClusterClient _client;
    private readonly ILogger<StreamController> _logger;
    
    public StreamController(IClusterClient client, ILogger<StreamController> logger)
    {
        _client = client;
        _logger = logger;
    }
    
    [HttpPost("{id}/stream")]
    public async Task Stream(string id, [FromBody] string value)
    {
        var streamId = GetStream(id, out var stream);

        _logger.LogInformation("Sending {Value} to {StreamId}", value, streamId);
        await stream.OnNextAsync(value);
    } 
    
    [HttpPost("{id}/start")]
    public async Task Start(string id)
    {
        var consumer = _client.GetGrain<IStreamConsumerGrain>(id);
        await consumer.StartReceive();
    }

    [HttpPost("{id}/stop")]
    public async Task Stop(string id)
    {
        var consumer = _client.GetGrain<IStreamConsumerGrain>(id);
        await consumer.StopReceive();
    }

    private StreamId GetStream(string id, out IAsyncStream<string> stream)
    {
        var provider = _client.GetStreamProvider(Constants.STREAM_PROVIDER_NAME);
        var streamId = StreamId.Create(Constants.STREAM_NAMESPACE, id);
        stream = provider.GetStream<string>(streamId);
        return streamId;
    }
}