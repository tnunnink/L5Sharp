using System.Collections.Generic;

namespace L5Sharp
{
    public interface IPredefined : IDataType
    {
        IEnumerable<IMember<IDataType>> Members { get; }
        IMember<IDataType> GetMember(string name);
        IMember<TType> GetMember<TType>(string name) where TType : IDataType;
    }
}