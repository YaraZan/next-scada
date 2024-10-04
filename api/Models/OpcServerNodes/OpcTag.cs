namespace api.Models.OpcServerNode
{
  public class OpcTag : OpcServerNode
  {
    public override string Type { get; } = "TAG";

    public OpcTag(string itemId, string itemName)
        : base(itemId, itemName)
    {
    }
  }
}
