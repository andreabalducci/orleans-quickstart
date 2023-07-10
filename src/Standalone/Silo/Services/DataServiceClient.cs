using Orleans.Runtime.Services;

namespace Standalone.Silo.Services;

public class DataServiceClient : GrainServiceClient<IDataService>, IDataServiceClient
{
    public DataServiceClient(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    // For convenience when implementing methods, you can define a property which gets the IDataService
    // corresponding to the grain which is calling the DataServiceClient.
    private IDataService GrainService => 
        // get local (to this silo => caller grain) grain service
        GetGrainService(CurrentGrainReference.GrainId);

    public ValueTask DumpToLocalDisk(DataSet dataSet) => GrainService.DumpToLocalDisk(dataSet);
}