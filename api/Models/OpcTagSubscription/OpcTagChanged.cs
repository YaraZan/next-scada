namespace api.Models.OpcItem
{
  public class OpcTagChanged(
    string tagId,
    object value,
    object quality,
    object timestamp
  )
  {
    public string TagId { get; set; } = tagId;
    public object Value { get; set; } = value;
    public object Quality { get; set; } = quality;

    public object Timestamp { get; set; } = timestamp;
  }
}
