namespace Standalone.Silo.Shared;

[GenerateSerializer]
public class DataSet
{
    public DataSet(string id)
    {
        Id = id;
    }

    [Id(0)]
    public string Id { get; }
    [Id(1)]
    public List<DataItem> Items { get; } = new();
    
    public void Add(string id, string name)
    {
        this.Items.Add(new DataItem(id, name));
    }
}