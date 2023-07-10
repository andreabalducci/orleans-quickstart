using Microsoft.AspNetCore.Mvc;
using Standalone.Silo.Counter;

namespace Standalone.Controllers;

[ApiController]
[Route("[controller]")]
public class CounterController
{
    private IClusterClient _client;

    public CounterController(IClusterClient client)
    {
        _client = client;
    }

    [HttpGet("{id}/value")]
    public async ValueTask<int> Read(string id)
    {
        var grain = _client.GetGrain<IStandaloneCounter>(id);
        return await grain.Read();
    }
}