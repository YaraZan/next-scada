namespace api.Models.OpcItem
{
  public class OpcItem(
    string itemId,
    object name,
    List<OpcItem>? children = null
  )
  {
    public string ItemId { get; set; } = itemId;
    public object Name { get; set; } = name;

    public object? Children { get; set; } = children;
  }
}
