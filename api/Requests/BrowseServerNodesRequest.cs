namespace api.Requests
{
  public class BrowseServerNodesRequest
  {
    public string ConnectionString { get; set; }

    public string? NodeId { get; set; } = null;
    public string Protocol { get; set; } = "DA";
    public string? Host { get; set; } = null;
  }

}
