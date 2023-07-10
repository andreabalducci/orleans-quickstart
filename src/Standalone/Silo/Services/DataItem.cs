namespace Standalone.Silo.Services;

[GenerateSerializer]
public class DataItem
{
    public DataItem(string id, string name)
    {
        Id = id;
        Name = name;
    }

    [Id(0)]
    public string Id { get;  }
    [Id(1)]
    public string Name { get;  }
}