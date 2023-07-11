namespace Standalone.Silo.Services.Generators;

public interface IStreamProducerGrain : IGrainWithStringKey
{
    ValueTask Produce(string value);
}