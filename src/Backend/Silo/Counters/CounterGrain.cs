using Shared.Grains;

namespace Backend.Silo.Counters;

public class CounterGrain : Grain, ICounterGrain
{
    private int _value;
    
    public Task Increment()
    {
        _value++;
        return Task.CompletedTask;
    }

    public Task Decrement()
    {
        _value--;
        return Task.CompletedTask;
    }

    public Task<int> Read()
    {
        return Task.FromResult(_value);
    }
}