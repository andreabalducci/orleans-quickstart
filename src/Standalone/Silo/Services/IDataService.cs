using Orleans.Services;

namespace Standalone.Silo.Services;

public interface IDataService : IGrainService
{
    ValueTask DumpToLocalDisk(DataSet dataSet);
}