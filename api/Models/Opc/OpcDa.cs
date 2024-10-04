namespace api.Models.Opc
{
  public class OpcDa : OpcServer
  {
    public override string Type => "DA";

    public OpcDa(
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
