using System.Collections.Generic;

namespace L5Sharp
{
    /// <summary>
    /// An implementation of <see cref="IDataType"/> that represents an immutable built in Logix type.  
    /// </summary>
    public interface IPredefined : IDataType
    {
        IEnumerable<IMember<IDataType>> Members { get; }
    }
}