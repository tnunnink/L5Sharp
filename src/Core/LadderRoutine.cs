using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class LadderRoutine : LogixComponent, ILadderRoutine
    {
        private readonly List<IRung> _rungs = new List<IRung>();

        public LadderRoutine(string name, string description = null) : base(name, description)
        {
        }
        
        public RoutineType Type => RoutineType.Ladder;
        public IEnumerable<IRung> Rungs => _rungs.AsEnumerable();
        
        public void AddRung(int number, string description, string text)
        {
            var rung = new Rung(number, description, text);
            AddRungComponent(rung);
        }

        public void InsertRung(int number, string description, string text)
        {
            throw new NotImplementedException();
        }

        public void RemoveRung(int number)
        {
            throw new NotImplementedException();
        }

        private void AddRungComponent(IRung rung)
        {
            if (rung == null) throw new ArgumentNullException(nameof(rung), "Rung can not be null");

            if (rung.Number > _rungs.Count)
                _rungs.Add(rung);

            _rungs.Insert(rung.Number, rung);
        }
        
        public void RemoveRungComponent(IRung rung)
        {
            if (rung == null) throw new ArgumentNullException(nameof(rung), "Rung can not be null");

            if (_rungs.All(r => r.Number != rung.Number)) return;
            
            _rungs.Remove(rung);
        }
    }
}