namespace LogixHelper
{
    public class Tag
    {
        public string Name { get; set; }
        public TagType TagType { get; set; }
        public DataType DataType { get; set; }
        public string Dimensions { get; set; }
        public string Class { get; set; } //todo enumeration?
        public Radix Radix { get; set; }
        public string AliasFor { get; set; }
        public string AliasBase { get; set; } //todo not on documentation but xsd
        public TagUsage Usage { get; set; }
        public bool IO { get; set; } //todo not on documentation but xsd
        public Scope Scope { get; set; }
        public bool CanForce { get; set; } //todo not on documentation but xsd
        public string FullName { get; set; } //todo not on documentation but xsd
        public bool Verified { get; set; } //todo not on documentation but xsd
        public bool Constant { get; set; }
        public ExternalAccess ExternalAccess { get; set; }
        public Data Data { get; set; }

        public string Description { get; set; }
    }
}