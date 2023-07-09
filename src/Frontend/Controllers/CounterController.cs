using Microsoft.AspNetCore.Mvc;
using Shared.Grains;

namespace Frontend.Controllers;

[ApiController]
[Route("[controller]")]
public class CounterController: ControllerBase
{
    private IClusterClient _client;

    public CounterController(IClusterClient client)
    {
        _client = client;
    }
    
    [HttpGet("{id}/value")]
    public async Task<int> Get(string id)
    {
        var grain = _client.GetGrain<ICounterGrain>(id);
        return await grain.Read();
    }
    
    [HttpPost("{id}/increment")]
    public async Task Increment(string id)
    {
        var grain = _client.GetGrain<ICounterGrain>(id);
        await grain.Increment();
    }
    
    [HttpPost("{id}/decrement")]
    public async Task Decrement(string id)
    {
        var grain = _client.GetGrain<ICounterGrain>(id);
        await grain.Decrement();
    }

    [HttpPost("load")]
    public async Task Load()
    {
        await Parallel.ForEachAsync(Enumerable.Range(1, 300_000), async (i, cancellation) =>
            {
                await Increment(i.ToString());
            }
        );
    }
}