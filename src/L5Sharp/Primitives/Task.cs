using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class Task : IXSerializable
    {
        private string _name;
        private ulong _rate;
        private ushort _priority;
        private readonly Dictionary<string, Program> _programs = new Dictionary<string, Program>();

        internal Task(string name, TaskType type = null, string description = null,
            ulong rate = 10, ushort priority = 10, ulong watchdog = 500,
            bool inhibitTask = false, bool disableUpdateOutputs = false)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Validate.TagName(name);

            Name = name;
            Type = type ?? TaskType.Continuous;
            Description = description ?? string.Empty;
            Rate = rate;
            Priority = priority;
            Watchdog = watchdog;
            InhibitTask = inhibitTask;
            DisableUpdateOutputs = disableUpdateOutputs;
        }

        private Task(XElement element)
        {
            Name = element.Attribute(nameof(Name))?.Value;
            Description = element.Element(nameof(Description))?.Value ?? string.Empty;
            Type = TaskType.FromName(element.Attribute(nameof(Type))?.Value);
            Rate = Convert.ToUInt64(element.Attribute(nameof(Rate))?.Value);
            Priority = Convert.ToUInt16(element.Attribute(nameof(Priority))?.Value);
            Watchdog = Convert.ToUInt64(element.Attribute(nameof(Watchdog))?.Value);
            InhibitTask = Convert.ToBoolean(element.Attribute(nameof(InhibitTask))?.Value);
            DisableUpdateOutputs = Convert.ToBoolean(element.Attribute(nameof(DisableUpdateOutputs))?.Value);
        }

        public string Name
        {
            get => _name;
            set
            {
                Validate.TagName(value);
                _name = value;
            }
        }

        public string Description { get; set; }
        public TaskType Type { get; set; }

        public ulong Rate
        {
            get => _rate;
            set
            {
                if (Type != TaskType.Periodic)
                    Throw.NotConfigurableException(nameof(Rate), nameof(Task), "Task is not periodic");

                _rate = value;
            }
        }

        public ushort Priority
        {
            get => _priority;
            set
            {
                if (Type == TaskType.Continuous)
                    Throw.NotConfigurableException(nameof(Priority), nameof(Task),
                        "Priority is available for Continuous Tasks");

                _priority = value;
            }
        }

        public ulong Watchdog { get; set; }
        public bool InhibitTask { get; set; }
        public bool DisableUpdateOutputs { get; set; }
        public IEnumerable<string> ScheduledPrograms => _programs.Keys.AsEnumerable();
        public IEnumerable<Program> Programs => _programs.Values.AsEnumerable();

        public void AddProgram(Program program)
        {
            if (program == null)
                throw new ArgumentNullException(nameof(program));

            if (_programs.ContainsKey(program.Name))
                Throw.NameCollisionException(program.Name);

            program.Task = this;
            _programs.Add(program.Name, program);
        }

        public void RemoveProgram(string name)
        {
            if (!_programs.ContainsKey(name)) return;
            _programs.Remove(name);
        }
        
        public void RenameProgram(string olfName, string newName)
        {
        }

        public XElement Serialize()
        {
            var element = new XElement(nameof(Task));
            element.Add(new XAttribute(nameof(Name), Name));
            element.Add(new XAttribute(nameof(Type), Type));
            element.Add(new XAttribute(nameof(Rate), Rate));
            element.Add(new XAttribute(nameof(Priority), Priority));
            element.Add(new XAttribute(nameof(Watchdog), Watchdog));
            element.Add(new XAttribute(nameof(InhibitTask), InhibitTask));
            element.Add(new XAttribute(nameof(DisableUpdateOutputs), DisableUpdateOutputs));

            if (!string.IsNullOrEmpty(Description))
                element.Add(new XElement(nameof(Description)), Description);

            if (_programs.Count <= 0) return element;

            var scheduled = new XElement(nameof(ScheduledPrograms));
            foreach (var name in _programs)
                scheduled.Add(new XElement("ScheduledProgram", new XAttribute("Name", name)));

            element.Add(scheduled);

            return element;
        }

        public Task Materialize(XElement element)
        {
            return new Task(element);
        }
    }
}