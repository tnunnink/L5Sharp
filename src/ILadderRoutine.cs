using System.Collections.Generic;

namespace L5Sharp
{
    public interface ILadderRoutine : IRoutine
    {
        IEnumerable<IRung> Rungs { get; }
    }
}