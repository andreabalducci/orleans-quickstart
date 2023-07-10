using Orleans.Services;

namespace Standalone.Silo.Services;

public interface ILocalDiskServiceClient : IGrainServiceClient<ILocalDiskService>, ILocalDiskService
{
}