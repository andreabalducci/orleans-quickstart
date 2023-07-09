namespace Shared.Grains;

public interface ICounterGrain : IGrainWithStringKey
{
    Task Increment();
    Task Decrement();
    Task<int> Read();
}