using Shared.Grains;

namespace Backend.Silo.Counters;

public class CounterGrain : Grain, ICounterGrain
{
    private int _value;
    
    public ValueTask Increment()
    {
        _value++;
        return ValueTask.CompletedTask;
    }

    public ValueTask Decrement()
    {
        _value--;
        return ValueTask.CompletedTask;
    }

    public ValueTask<int> Read()
    {
        return ValueTask.FromResult(_value);
    }
}