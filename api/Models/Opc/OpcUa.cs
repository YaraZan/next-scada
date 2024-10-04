namespace api.Models.Opc
{
  public class OpcUa : OpcServer
  {
    public override string Type => "UA";

    public OpcUa(
      string name,
      string url,
      string connectionString,
      string? host = null
    ) : base(
      name,
      url,
      connectionString,
      host
    )
    { }
  }
}
