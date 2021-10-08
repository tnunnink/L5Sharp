using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Base
{
    public abstract class ProgramBase : ComponentBase, IProgram
    {
        private readonly Dictionary<string, Tag> _tags = new Dictionary<string, Tag>();
        private readonly Dictionary<string, IRoutine> _routines = new Dictionary<string, IRoutine>();

        private readonly Dictionary<Type, RoutineType> _routineTypes = new Dictionary<Type, RoutineType>
        {
            { typeof(RllRoutine), RoutineType.Ladder }
        };

        protected ProgramBase(string name, string description = null, bool testEdits = false, bool disabled = false) 
            : base(name, description)
        {
            TestEdits = testEdits;
            Disabled = disabled;
        }

        public abstract ProgramType Type { get; }
        
        public bool TestEdits { get; set; }

        public bool Disabled { get; set; }

        public IEnumerable<Tag> Tags => _tags.Values.AsEnumerable();

        public IEnumerable<IRoutine> Routines => _routines.Values.AsEnumerable();


        public bool HasRoutine(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            return _routines.Keys.Any(r => r == name);
        }

        public bool HasRoutine(RoutineType type)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException(nameof(type));
            return _routines.Values.Any(r => r.Type == type);
        }

        public IRoutine GetRoutine(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            _routines.TryGetValue(name, out var routine);
            return routine;
        }

        public IEnumerable<T> GetRoutines<T>() where T : IRoutine
        {
            var type = _routineTypes.ContainsKey(typeof(T)) ? _routineTypes[typeof(T)] : null;

            if (type == null)
                throw new ArgumentException($"Not RoutineType defined for type '{typeof(T)}'");

            return _routines.Values.Where(r => r.Type == type).Select(r => (T)r);
        }

        public void AddRoutine(string name, RoutineType type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type), "RoutineType can not be null");

            var routine = type.Create(name);
            if (routine == null) return;

            if (_routines.ContainsKey(routine.Name))
                Throw.ComponentNameCollisionException(routine.Name, typeof(IRoutine));

            _routines.Add(routine.Name, routine);
        }

        public void RemoveRoutine(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name can not be null");

            if (!_routines.ContainsKey(name)) return;

            _routines.Remove(name);
        }

        public void AddTag(Tag tag)
        {
        }

        public void RemoveTag(string name)
        {
        }
    }
}