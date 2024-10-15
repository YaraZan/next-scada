namespace api.Models.OpcServerNode
{
  public class OpcFolder(string id, string name) : OpcServerNode(id, name)
  {
    public override string Type { get; } = "FOLDER";
  }
}
