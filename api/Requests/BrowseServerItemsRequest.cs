namespace api.Requests
{
  public class BrowseServerItemsRequest
  {
    public string ConnectionString { get; set; }

    public string? NodeId { get; set; } = null;
    public bool IsDa { get; set; } = false;
    public string? Host { get; set; } = null;
  }

}
