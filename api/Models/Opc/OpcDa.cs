namespace api.Models.Opc
{
    public class OpcDa(
        string description,
        string progId,
        string urlString
        ) : OpcServer
    {
    public override string Url { get; set; } = urlString;
    public override string Name { get; set; } = description;
    public override string ConnectionString { get; set; } = progId;

    public override string Type => "DA";
    }
}
