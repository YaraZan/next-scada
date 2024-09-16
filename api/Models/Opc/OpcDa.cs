namespace api.Models.Opc
{
    public class OpcDa : Opc
    {
        public OpcDa(
            string description,
            string progId,
            string urlString
        )
        {
            Name = description;
            ConnectionString = progId;
            Url = urlString;
        }

        public override string Url { get; set; }
        public override string Name { get; set; }
        public override string ConnectionString { get; set; }

        public override string Type => "DA";
    }
}
