namespace api.Models.Opc
{
  public abstract class OpcServer(
    string name,
    string url,
    string connectionString,
    string? host = null
    )
  {
    public string Name { get; set; } = name;
    public string Url { get; set; } = url;
    public string ConnectionString { get; set; } = connectionString;

    public string? Host { get; set; } = host;

    public abstract string Type { get; }
  }
}
