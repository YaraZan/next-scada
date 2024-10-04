namespace api.Models.OpcServerNode
{
  public abstract class OpcServerNode
  {
    public string ItemId { get; set; }
    public string ItemName { get; set; }

    public abstract string Type { get; }

    protected OpcServerNode(string itemId, string itemName)
    {
      ItemId = itemId;
      ItemName = itemName;
    }
  }
}
