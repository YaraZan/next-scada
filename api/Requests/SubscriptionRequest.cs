namespace api.Requests
{
  public class SubscriptionRequest
  {
    public string ConnectionString { get; set; }
    public string TagId { get; set; }
    public string Protocol { get; set; } = "DA";
    public string? Host { get; set; }
  }

}
