namespace Standalone.Silo.Generator;

public interface IGeneratorGrain : IGrainWithStringKey
{
    ValueTask Generate(int seed);
}