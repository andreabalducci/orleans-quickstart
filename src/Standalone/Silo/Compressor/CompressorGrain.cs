using Orleans.Concurrency;
using Standalone.Silo.Services;
using Standalone.Silo.Shared;

namespace Standalone.Silo.Compressor;

// default by the number of CPU cores on the machine
[StatelessWorker(2)]
public class CompressorGrain : Grain, ICompressorGrain
{
    private readonly int _id;
    private static int _nextId = 0;
    private readonly ILogger<CompressorGrain> _logger;
    
    public CompressorGrain(ILogger<CompressorGrain> logger)
    {
        _logger = logger;
        _id = Interlocked.Increment(ref _nextId);
    }
    
    public async ValueTask<byte[]> Compress(DataSet data)
    {
        _logger.LogInformation("Compressing {Id} on {WorkerId}", data.Id, _id);
        await Task.Delay(10_000);
        _logger.LogInformation("Compression completed for {Id} on {WorkerId}", data.Id, _id);

        return Array.Empty<byte>();
    }
}