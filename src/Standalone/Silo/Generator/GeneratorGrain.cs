using Standalone.Silo.Services;

namespace Standalone.Silo.Generator;

public interface IGeneratorGrain : IGrainWithStringKey
{
    ValueTask Generate(int seed);
}

public class GeneratorGrain : Grain, IGeneratorGrain
{
    private readonly IDataServiceClient _dataService;

    public GeneratorGrain(IDataServiceClient dataService)
    {
        _dataService = dataService;
    }

    public async ValueTask Generate(int seed)
    {
        var id = this.GetPrimaryKeyString();
        
        var data = new DataSet(id);
        for (int c = 0; c <= 10; c++)
        {
            data.Add(c.ToString(), Guid.NewGuid().ToString());
        }
        
        await _dataService.DumpToLocalDisk(data);
    }
}