using Orleans.Services;

namespace Standalone.Silo.Services;

public interface IDataServiceClient : IGrainServiceClient<IDataService>, IDataService
{
}