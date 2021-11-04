using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IPredefined : IDataType
    {
        public IMember GetMember(string name);
    }
}