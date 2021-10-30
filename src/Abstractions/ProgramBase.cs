using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class ProgramBase : LogixComponent, IProgram
    {
        private readonly Dictionary<string, ITag> _tags = new Dictionary<string, ITag>();
        private readonly Dictionary<string, IRoutine> _routines = new Dictionary<string, IRoutine>();

        private readonly Dictionary<Type, RoutineType> _routineTypes = new Dictionary<Type, RoutineType>
        {
            { typeof(LadderRoutine), RoutineType.Ladder }
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

        public IEnumerable<ITag> Tags => _tags.Values.AsEnumerable();

        public IEnumerable<IRoutine> Routines => _routines.Values.AsEnumerable();
        
        public bool HasRoutine(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            return _routines.ContainsKey(name);
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

        public TRoutine GetRoutine<TRoutine>(string name) where TRoutine : IRoutine
        {
            throw new NotImplementedException();
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

        public ITag GetTag(string name)
        {
            return GetTagComponent(name);
        }

        public void AddTag(string name, IDataType dataType)
        {
            var tag = new Tag(name, dataType, parent: this);
            AddTagComponent(tag);
        }

        public void RemoveTag(string name) => RemoveTagComponent(GetTagComponent(name));

        private ITag GetTagComponent(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name), "Name can not be null");

            _tags.TryGetValue(name, out var tag);
            return tag;
        }

        private void AddTagComponent(ITag tag)
        {
            if (tag == null) throw new ArgumentNullException(nameof(tag), "Tag can not be null");

            if (_tags.ContainsKey(tag.Name))
                Throw.ComponentNameCollisionException(tag.Name, typeof(ITag));

            _tags.Add(tag.Name, tag);
        }

        private void RemoveTagComponent(ITag tag)
        {
            if (tag == null) throw new ArgumentNullException(nameof(tag), "Tag can not be null");

            if (!_tags.ContainsKey(tag.Name)) return;

            _tags.Remove(tag.Name);
        }
    }
}