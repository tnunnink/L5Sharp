using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class LadderRoutine : ILadderRoutine
    {
        private readonly List<IRung> _rungs = new List<IRung>();

        public LadderRoutine(string name, string description = null)
        {
        }

        public ComponentName Name { get; }
        public string Description { get; }
        public RoutineType Type => RoutineType.Ladder;
        public IEnumerable<IRung> Rungs => _rungs.AsEnumerable();
    }
}