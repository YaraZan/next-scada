namespace api.Models.OpcServerNode
{
  public class OpcFolder : OpcServerNode
  {
    public override string Type { get; } = "FOLDER";

    public OpcFolder(string itemId, string itemName)
        : base(itemId, itemName)
    {
    }
  }
}
