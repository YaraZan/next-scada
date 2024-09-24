namespace api.Models.Opc
{
    public class OpcUa(
        string applicationName,
        string discoveryUri,
        string applicationUriString,
        string? host = null
        ) : OpcServer
    {
    public override string Url { get; set; } = applicationUriString;
    public override string Name { get; set; } = applicationName;
    public override string ConnectionString { get; set; } = discoveryUri;

    public override string? Host { get; set; } = host;

    public override string Type => "UA";
    }
}
