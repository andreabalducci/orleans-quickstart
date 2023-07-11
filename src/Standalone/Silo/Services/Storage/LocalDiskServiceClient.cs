using Orleans.Runtime.Services;
using Standalone.Silo.Shared;

namespace Standalone.Silo.Services.Storage;

public class LocalDiskServiceClient : GrainServiceClient<ILocalDiskService>, ILocalDiskServiceClient
{
    public LocalDiskServiceClient(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    // For convenience when implementing methods, you can define a property which gets the IDataService
    // corresponding to the grain which is calling the DataServiceClient.
    private ILocalDiskService GrainService => 
        // get local (to this silo => caller grain) grain service
        GetGrainService(CurrentGrainReference.GrainId);

    public ValueTask Save(DataSet dataSet) => GrainService.Save(dataSet);
}