using System.Collections;
using L5Sharp.Enums;

namespace L5Sharp;

public interface ILogixCode : IList
{
    RoutineType Type { get; }
}