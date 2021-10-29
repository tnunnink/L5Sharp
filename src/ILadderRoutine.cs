using System.Collections.Generic;

namespace L5Sharp
{
    public interface ILadderRoutine : IRoutine
    {
        IEnumerable<IRung> Rungs { get; }
        void AddRung(int number, string description, string text);
        void RemoveRung(int number);
    }
}