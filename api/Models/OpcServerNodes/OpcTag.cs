namespace api.Models.OpcServerNode
{
  public class OpcTag(string nodeId, string nodeName) : OpcServerNode(nodeId, nodeName)
  {
    public override string Type { get; } = "TAG";
  }
}
