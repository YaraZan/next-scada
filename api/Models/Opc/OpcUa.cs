namespace api.Models.Opc
{
  public class OpcUa(
    string name,
    string url,
    string connectionString,
    string? host = null
    ) : OpcServer(
    name,
    url,
    connectionString,
    host
    )
  {
    public override string Type => "UA";
  }
}
