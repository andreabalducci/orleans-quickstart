using Orleans.Services;

namespace Standalone.Silo.Services.Storage;

public interface ILocalDiskServiceClient : IGrainServiceClient<ILocalDiskService>, ILocalDiskService
{
}