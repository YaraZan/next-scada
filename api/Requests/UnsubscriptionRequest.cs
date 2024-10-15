namespace api.Requests
{
  public class UnsubscriptionRequest
  {
    public string Protocol { get; set; } = "DA";

    public int TagId { get; set; }
  };
}
