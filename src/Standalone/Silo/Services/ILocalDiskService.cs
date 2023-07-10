using Orleans.Services;
using Standalone.Silo.Shared;

namespace Standalone.Silo.Services;

public interface ILocalDiskService : IGrainService
{
    ValueTask Save(DataSet dataSet);
}