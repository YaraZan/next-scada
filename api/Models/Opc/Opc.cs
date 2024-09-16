namespace api.Models.Opc
{
    public abstract class Opc
    {
        public virtual string Url { get; set; }
        public virtual string Name { get; set; }
        public virtual string ConnectionString { get; set; }

        public abstract string Type { get; }
    }
}
