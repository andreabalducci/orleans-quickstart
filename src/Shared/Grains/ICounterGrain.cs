namespace Shared.Grains;

public interface ICounterGrain : IGrainWithStringKey
{
    ValueTask Increment();
    ValueTask Decrement();
    ValueTask<int> Read();
}