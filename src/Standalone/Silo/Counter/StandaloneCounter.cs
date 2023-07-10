namespace Standalone.Silo.Counter;

public interface IStandaloneCounter : IGrainWithStringKey
{
    ValueTask<int> Read();
}

public class StandaloneCounter : Grain,IStandaloneCounter
{
    public ValueTask<int> Read()
    {
        return new ValueTask<int>(10);
    }
}