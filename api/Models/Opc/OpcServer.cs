namespace api.Models.Opc
{
    public abstract class OpcServer
    {
        public virtual string Url { get; set; }
        public virtual string Name { get; set; }
        public virtual string ConnectionString { get; set; }

        public virtual string? Host { get; set; }

        public abstract string Type { get; }
    }
}
