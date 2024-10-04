namespace api.Models.OpcItem
{
  public class OpcTagErrored(
    string tagId,
    string errorMessage
  )
  {
    public string TagId { get; set; } = tagId;
    public object ErrorMessage { get; set; } = errorMessage;
  }
}
