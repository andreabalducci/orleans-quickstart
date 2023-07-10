using System.Runtime.InteropServices.JavaScript;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace Standalone.Silo.Services;

[Reentrant]
public class DataService : GrainService, IDataService
{
    private readonly Orleans.Runtime.Silo _silo;
    readonly IGrainFactory _grainFactory;
    private readonly ILogger<DataService> _logger;

    public DataService(
        IServiceProvider services,
        GrainId id,
        Orleans.Runtime.Silo silo,
        ILoggerFactory loggerFactory,
        IGrainFactory grainFactory)
        : base(id, silo, loggerFactory)
    {
        _silo = silo;
        _grainFactory = grainFactory;
        _logger = loggerFactory.CreateLogger<DataService>();
    }

    public override Task Init(IServiceProvider serviceProvider) =>
        base.Init(serviceProvider);

    public override async Task Start()
    {
        await base.Start();
    }

    public override async Task Stop()
    {
        await base.Stop();
    }

    public async ValueTask DumpToLocalDisk(DataSet dataSet)
    {
        _logger.LogInformation("Dump started");
        await Task.Delay(1_000);
        _logger.LogInformation("Dump completed");
    }
}