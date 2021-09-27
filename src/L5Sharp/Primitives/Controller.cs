using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Builders;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class Controller : IController, IXSerializable
    {
        private readonly Dictionary<string, IDataType> _dataTypes = new Dictionary<string, IDataType>();
        private readonly Dictionary<string, Module> _modules = new Dictionary<string, Module>();
        private readonly Dictionary<string, Instruction> _instructions = new Dictionary<string, Instruction>();
        private readonly Dictionary<string, Tag> _tags = new Dictionary<string, Tag>();
        private readonly Dictionary<string, Program> _programs = new Dictionary<string, Program>();
        private readonly Dictionary<string, Task> _tasks = new Dictionary<string, Task>();

        public Controller(string name, string description = null, string processorType = null,
            ulong majorRev = 0, ushort minorRev = 0)
        {
            Name = name;
            Use = Use.Target;
            Description = description ?? string.Empty;
            ProcessorType = processorType ?? string.Empty;
            MajorRev = majorRev;
            MinorRev = minorRev;
            ProjectCreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }

        public Controller(XElement element)
        {
            Name = element.Attribute(nameof(Name))?.Value;
            Description = element.Attribute(nameof(Description))?.Value;
            ProcessorType = element.Attribute(nameof(ProcessorType))?.Value;
            MajorRev = Convert.ToUInt64(element.Attribute(nameof(MajorRev))?.Value);
            MinorRev = Convert.ToUInt16(element.Attribute(nameof(MinorRev))?.Value);
            ProjectCreationDate = Convert.ToDateTime(element.Attribute(nameof(ProjectCreationDate))?.Value);
            LastModifiedDate = Convert.ToDateTime(element.Attribute(nameof(LastModifiedDate))?.Value);

            ParseDataTypes(element);
            ParsePrograms(element);
            //...
        }

        public string Name { get; }

        public Use Use { get; set; }
        public string Description { get; }
        public string ProcessorType { get; }
        public ulong MajorRev { get; }
        public ushort MinorRev { get; }
        public DateTime ProjectCreationDate { get; }
        public DateTime LastModifiedDate { get; }
        public IEnumerable<IDataType> DataTypes => _dataTypes.Values.AsEnumerable();

        public IEnumerable<Module> Modules { get; }

        public IEnumerable<Instruction> Instructions { get; }

        public IEnumerable<Tag> Tags { get; }

        public IEnumerable<Program> Programs { get; }

        public IEnumerable<Task> Tasks => _tasks.Values.AsEnumerable();

        public bool HasContinuousTask => _tasks.Any(t => t.Value.Type == TaskType.Continuous);

        public IControllerCreator Create()
        {
            return new ControllerCreator(this);
        }

        public void AddDataType(DataType dataType)
        {
            if (dataType == null)
                throw new ArgumentNullException(nameof(dataType));

            if (_dataTypes.ContainsKey(dataType.Name))
                throw new InvalidOperationException();

            _dataTypes.Add(dataType.Name, dataType);
        }

        public void RemoveDataType(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (!_dataTypes.ContainsKey(name))
                Throw.ItemNotFoundException(name);

            _dataTypes.Remove(name);
        }

        public void AddTask(Task task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (_tasks.ContainsKey(task.Name))
                Throw.NameCollisionException(task.Name);

            if (HasContinuousTask && task.Type == TaskType.Continuous)
                throw new InvalidOperationException();

            _tasks.Add(task.Name, task);
        }

        public void DeleteTask(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (!_tasks.ContainsKey(name))
                Throw.ItemNotFoundException(name);

            _tasks.Remove(name);
        }


        public XElement Serialize()
        {
            var element = new XElement(nameof(Controller));
            element.Add(new XAttribute(nameof(Name), Name));
            element.Add(new XAttribute(nameof(ProcessorType), ProcessorType));
            element.Add(new XAttribute(nameof(MajorRev), MajorRev));
            element.Add(new XAttribute(nameof(MinorRev), MinorRev));
            //todo update approval tests to scrub text or have datetime generator that we can mock...
            //element.Add(new XAttribute(nameof(ProjectCreationDate), ProjectCreationDate));
            //element.Add(new XAttribute(nameof(LastModifiedDate), LastModifiedDate));

            element.Add(new XElement(nameof(DataTypes)), DataTypes.Select(d => d.Serialize()));

            return element;
        }

        private void ParseDataTypes(XContainer element)
        {
            var dataTypes = element.Element(nameof(DataTypes))?.Descendants().Select(DataType.Materialize);
            if (dataTypes == null) return;
            foreach (var dataType in dataTypes)
                _dataTypes.Add(dataType.Name, dataType);
        }

        private void ParsePrograms(XContainer element)
        {
            var programs = element.Element(nameof(Programs))?.Descendants().Select(Program.Materialize);
            if (programs == null) return;
            foreach (var program in programs)
                _programs.Add(program.Name, program);
        }
    }
}