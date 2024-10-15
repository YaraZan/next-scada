namespace api.Models.OpcServerNode
{
  public abstract class OpcServerNode(
    string id,
    string name
  )
  {
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;

    public abstract string Type { get; }
  }
}
