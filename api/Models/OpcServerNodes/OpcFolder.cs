namespace api.Models.OpcServerNode
{
  public class OpcFolder(string nodeId, string nodeName) : OpcServerNode(nodeId, nodeName)
  {
    public override string Type { get; } = "FOLDER";
  }
}
