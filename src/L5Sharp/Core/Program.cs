using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Program : IComponent
    {
        private readonly Dictionary<string, Tag> _tags = new Dictionary<string, Tag>();
        private readonly Dictionary<string, Routine> _routines = new Dictionary<string, Routine>();

        public Program(string name, ProgramType type = null, string description = null, string mainRoutineName = null,
            string faultRoutineName = null, bool testEdits = false, bool disabled = false, bool useAsFolder = false)
        {
            Name = name;
            Type = type ?? ProgramType.Normal;
            Description = description ?? string.Empty;
            MainRoutineName = mainRoutineName ?? string.Empty;
            FaultRoutineName = faultRoutineName ?? string.Empty;
            TestEdits = testEdits;
            Disabled = disabled;
            UseAsFolder = useAsFolder;
        }

        internal Program(XElement element)
        {
            Name = element.Attribute(nameof(Name))?.Value;
            Description = element.Attribute(nameof(Description))?.Value;
            Type = ProgramType.FromName(element.Attribute(nameof(Type))?.Value);
            MainRoutineName = element.Attribute(nameof(MainRoutineName))?.Value;
            FaultRoutineName = element.Attribute(nameof(FaultRoutineName))?.Value;
            TestEdits = Convert.ToBoolean(element.Attribute(nameof(TestEdits))?.Value);
            Disabled = Convert.ToBoolean(element.Attribute(nameof(Disabled))?.Value);
            UseAsFolder = Convert.ToBoolean(element.Attribute(nameof(UseAsFolder))?.Value);
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ProgramType Type { get; }
        public string MainRoutineName { get; set; }
        public string FaultRoutineName { get; set; }
        public bool TestEdits { get; }
        public bool Disabled { get; set; }
        public bool UseAsFolder { get; set; }
        public IEnumerable<Tag> Tags => _tags.Values.AsEnumerable();
        public IEnumerable<Routine> Routines => _routines.Values.AsEnumerable();

        public void AddRoutine(Routine routine)
        {
            if (routine == null) throw new ArgumentNullException(nameof(routine));
            
            if (_routines.ContainsKey(routine.Name))
                Throw.ComponentNameCollisionException(routine.Name, typeof(Routine));
            
            _routines.Add(routine.Name, routine);
        }
        
        public void AddRoutine(string name, RoutineType type)
        {
            if (string.IsNullOrEmpty(name))
                Throw.ArgumentNullOrEmptyException(nameof(name));

            var routine = new Routine(name, type);
            AddRoutine(routine);
        }
        
        public void RemoveRoutine(string name)
        {
            if (string.IsNullOrEmpty(name))
                Throw.ArgumentNullOrEmptyException(nameof(name));
            
            if (!_routines.ContainsKey(name!))
                Throw.ComponentNotFoundException(name, typeof(Routine));
            
            _routines.Remove(name);
        }

        public void RenameRoutine(string oldName, string newName)
        {
            if (string.IsNullOrEmpty(oldName))
                Throw.ArgumentNullOrEmptyException(nameof(oldName));
            
            if (!_routines.ContainsKey(oldName!))
                Throw.ComponentNotFoundException(oldName, typeof(Routine));

            var routine = _routines[oldName];
            _routines.Remove(oldName);

            routine.Name = newName;
            _routines.Add(routine.Name, routine);
        }

        public void AddTag(Tag tag)
        {
            
        }
        
        public void AddTag(string name, IDataType dataType)
        {
            
        }
        
        public void RemoveTag(string name)
        {
            
        }
        
        public void UpdateTag(Tag tag)
        {
            
        }
        
        public void RenameTag(string oldName, string newName)
        {
            
        }
        
        public XElement Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}