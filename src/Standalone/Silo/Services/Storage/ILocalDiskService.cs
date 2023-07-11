using Orleans.Services;
using Standalone.Silo.Shared;

namespace Standalone.Silo.Services.Storage;

public interface ILocalDiskService : IGrainService
{
    ValueTask Save(DataSet dataSet);
}