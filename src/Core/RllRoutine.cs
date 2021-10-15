using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class RllRoutine : ComponentBase, IRoutine
    {
        private readonly List<Rung> _rungs = new List<Rung>();

        public RllRoutine(string name, string description = null) : base(name, description)
        {
        }
        
        public RoutineType Type => RoutineType.Ladder;
        public IEnumerable<Rung> Rungs => _rungs.AsEnumerable();

        public void AddRung(Rung rung)
        {
            if (rung == null) throw new ArgumentNullException(nameof(rung), "Rung can not be null");

            if (rung.Number > _rungs.Count)
                _rungs.Add(rung);

            _rungs.Insert(rung.Number, rung);
        }
        
        public void RemoveRung(Rung rung)
        {
            if (rung == null) throw new ArgumentNullException(nameof(rung), "Rung can not be null");

            if (_rungs.All(r => r.Number != rung.Number)) return;
            
            _rungs.Remove(rung);
        }
    }
}