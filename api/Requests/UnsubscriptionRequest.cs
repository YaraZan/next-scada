namespace api.Requests
{
  public class UnsubscriptionRequest
  {
    public bool IsDa { get; set; }

    public IEnumerable<int> SubscribedItemIds { get; set; } = [];
  };
}
