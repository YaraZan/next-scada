namespace api.Requests
{
  public class SubscriptionRequest
  {
    public string ConnectionString { get; set; }
    public string TagId { get; set; }
    public bool IsDa { get; set; } = false;
    public string? Host { get; set; }
  }

}
