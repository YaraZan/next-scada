namespace api.Models.Opc
{
    public class OpcDa(
        string description,
        string progId,
        string urlString,
        string? host = null
    ) : OpcServer
    {
        public override string Url { get; set; } = urlString;
        public override string Name { get; set; } = description;
        public override string ConnectionString { get; set; } = progId;

        public override string? Host { get; set; } = host;

        public override string Type => "DA";
    }
}
