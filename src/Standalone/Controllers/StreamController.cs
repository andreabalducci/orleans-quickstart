using Microsoft.AspNetCore.Mvc;
using Orleans.Runtime;
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
        var provider = _client.GetStreamProvider(Constants.STREAM_PROVIDER_NAME);
        
        var streamId = StreamId.Create(Constants.STREAM_NAMESPACE, id);
        var stream = provider.GetStream<string>(streamId);
        
        _logger.LogInformation("Sending {Value} to {StreamId}", value, streamId);
        await stream.OnNextAsync(value);
    }
}