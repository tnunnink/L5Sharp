using L5Sharp.Primitives;

namespace L5Sharp.Builders
{
    public interface IDataTypeBuilder
    {
        public DataType Build();
        IDataTypeBuilder WithMember(string name, string dataType, string description = null);
    }
}