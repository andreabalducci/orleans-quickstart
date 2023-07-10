using Orleans.TestingHost;
using Standalone.Silo.Counter;

namespace Standalone.Tests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var builder = new TestClusterBuilder();
        var cluster = builder.Build();
        await cluster.DeployAsync();

        var hello = cluster.GrainFactory.GetGrain<IStandaloneCounter>("a");
        var value = await hello.Read();

        await cluster.StopAllSilosAsync();

        Assert.Equal(10, value);
    }
}