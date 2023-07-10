using Standalone.Silo.Services;
using Standalone.Silo.Shared;

namespace Standalone.Silo.Compressor;

public interface ICompressorGrain : IGrainWithStringKey
{
    ValueTask<byte[]> Compress(DataSet data);
}