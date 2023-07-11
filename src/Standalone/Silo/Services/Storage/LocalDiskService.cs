using Orleans.Concurrency;
using Orleans.Runtime;
using Standalone.Silo.Compressor;
using Standalone.Silo.Shared;

namespace Standalone.Silo.Services.Storage;

[Reentrant]
public class LocalDiskService : GrainService, ILocalDiskService
{
    private readonly Orleans.Runtime.Silo _silo;
    readonly IGrainFactory _grainFactory;
    private readonly ILogger<LocalDiskService> _logger;

    public LocalDiskService(
        IServiceProvider services,
        GrainId id,
        Orleans.Runtime.Silo silo,
        ILoggerFactory loggerFactory,
        IGrainFactory grainFactory)
        : base(id, silo, loggerFactory)
    {
        _silo = silo;
        _grainFactory = grainFactory;
        _logger = loggerFactory.CreateLogger<LocalDiskService>();
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

    public async ValueTask Save(DataSet dataSet)
    {
        var compressor = _grainFactory.GetGrain<ICompressorGrain>("pool1");
        
        _logger.LogInformation("Save started for {DataSet}", dataSet.Id);
        await Task.Delay(10_000);

        var compressed = await compressor.Compress(dataSet);
        _logger.LogInformation("Save completed for {DataSet}", dataSet.Id);
    }
}