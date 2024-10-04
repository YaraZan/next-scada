namespace api.Models.Opc
{
  public abstract class OpcServer
  {
    public string Name { get; set; }
    public string Url { get; set; }
    public string ConnectionString { get; set; }

    public string? Host { get; set; }

    public abstract string Type { get; }

    protected OpcServer(
      string name,
      string url,
      string connectionString,
      string? host = null
    )
    {
      Name = name;
      Url = url;
      ConnectionString = connectionString;
      Host = host;
    }
  }
}
