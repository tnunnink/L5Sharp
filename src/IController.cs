using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IController : ILogixComponent
    {
        ProcessorType ProcessorType { get; }
        Revision Revision { get; }
        IDataTypes DataTypes { get; }
        IEnumerable<ITag> Tags { get; }
        IEnumerable<IProgram> Programs { get; }
        ITasks Tasks { get; }
    }
}