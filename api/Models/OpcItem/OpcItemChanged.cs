namespace api.Models.OpcItem
{
  public class OpcItemChanged(
    string itemId,
    object value,
    object quality,
    object timestamp
  )
  {
    public string ItemId { get; set; } = itemId;
    public object Value { get; set; } = value;
    public object Quality { get; set; } = quality;

    public object Timestamp { get; set; } = timestamp;
  }
}
