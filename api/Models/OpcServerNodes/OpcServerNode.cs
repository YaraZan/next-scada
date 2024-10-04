namespace api.Models.OpcServerNode
{
  public abstract class OpcServerNode(
    string nodeId,
    string nodeName
  )
  {
    public string NodeId { get; set; } = nodeId;
    public string NodeName { get; set; } = nodeName;

    public abstract string Type { get; }
  }
}
