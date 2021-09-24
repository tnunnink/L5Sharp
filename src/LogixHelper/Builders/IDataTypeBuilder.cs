using LogixHelper.Primitives;

namespace LogixHelper.Builders
{
    public interface IDataTypeBuilder
    {
        public DataType Build();
        IDataTypeBuilder WithMember(string name, string dataType, string description = null);
    }
}