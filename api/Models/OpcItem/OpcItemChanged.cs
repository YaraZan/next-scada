namespace api.Models.OpcItem
{
  public class OpcItemChanged(
    string itemPath,
    object value,
    object quality,
    object timestamp
  )
  {
    public string ItemPath { get; set; } = itemPath;
    public object Value { get; set; } = value;
    public object Quality { get; set; } = quality;

    public object Timestamp { get; set; } = timestamp;
  }
}
