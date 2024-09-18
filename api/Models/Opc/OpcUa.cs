namespace api.Models.Opc
{
    public class OpcUa : OpcServer
    {
        public OpcUa(
            string applicationName,
            string discoveryUri,
            string applicationUriString
        )
        {
            Name = applicationName;
            ConnectionString = discoveryUri;
            Url = applicationUriString;
        }

        public override string Url { get; set; }
        public override string Name { get; set; }
        public override string ConnectionString { get; set; }

        public override string Type => "UA";
    }
}
