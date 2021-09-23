using LogixHelper.Primitives;

namespace LogixHelper.Abstractions
{
    public interface IDataTypeBuilder
    {
        public DataType Build();
        IDataTypeBuilder WithMember(string name, string dataType, string description = null);
    }
}