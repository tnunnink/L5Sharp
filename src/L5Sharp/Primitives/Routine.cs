using System;
using System.Collections.Generic;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class Routine
    {
        private readonly Dictionary<int, Rung> _rungs = new Dictionary<int, Rung>();
        public Routine(string name, RoutineType type)
        {
            if (string.IsNullOrEmpty(name))
                Throw.ArgumentNullOrEmptyException(nameof(name));

            if (type == null) throw new ArgumentNullException(nameof(type));
            
            Name = name;
            Type = type;
        }
        public string Name { get; set; }
        public RoutineType Type { get; set; }
        public IEnumerable<Rung> Rungs => _rungs.Values;

        public void AddRung(string text)
        {
            
        }
        
        public void DeleteRung(string text)
        {
            
        }
        
        public void InsertRung(string text)
        {
            
        }
        
        public void EditRung(string text)
        {
            
        }
    }
}