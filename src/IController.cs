using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IController : ILogixComponent
    {
        ProcessorType ProcessorType { get; }
        Revision Revision { get; }
        IDataTypes DataTypes { get; }
        ITags Tags { get; }
        IPrograms Programs { get; }
        ITasks Tasks { get; }
    }
}