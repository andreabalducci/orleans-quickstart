using Microsoft.AspNetCore.Mvc;
using Standalone.Silo.Generator;

namespace Standalone.Controllers;

[ApiController]
[Route("[controller]")]
public class GeneratorController : ControllerBase
{
    private IClusterClient _client;

    public GeneratorController(IClusterClient client)
    {
        _client = client;
    }

    [HttpPost("{id}/generate")]
    public async Task Generate(string id)
    {
        var grain = _client.GetGrain<IGeneratorGrain>(id);
        await grain.Generate(0);
    }
}