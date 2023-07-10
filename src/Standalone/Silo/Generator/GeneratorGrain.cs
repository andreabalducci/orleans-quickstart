using Standalone.Silo.Services;
using Standalone.Silo.Shared;

namespace Standalone.Silo.Generator;

public class GeneratorGrain : Grain, IGeneratorGrain
{
    private readonly ILocalDiskServiceClient _localDiskService;

    public GeneratorGrain(ILocalDiskServiceClient localDiskService)
    {
        _localDiskService = localDiskService;
    }

    public async ValueTask Generate(int seed)
    {
        var id = this.GetPrimaryKeyString();
        
        var data = new DataSet(id);
        for (int c = 0; c <= 10; c++)
        {
            data.Add(c.ToString(), Guid.NewGuid().ToString());
        }
        
        await _localDiskService.Save(data);
    }
}