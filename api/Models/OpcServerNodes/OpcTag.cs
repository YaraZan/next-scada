namespace api.Models.OpcServerNode
{
  public class OpcTag(string id, string name) : OpcServerNode(id, name)
  {
    public override string Type { get; } = "TAG";
  }
}
