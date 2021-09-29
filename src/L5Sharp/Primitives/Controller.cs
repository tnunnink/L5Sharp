using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Builders;
using L5Sharp.Builders.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class Controller : IController, IXSerializable
    {
        private string _name;
        private readonly Dictionary<Type, dynamic> _components = new Dictionary<Type, dynamic>();
        private readonly Dictionary<string, IDataType> _dataTypes = new Dictionary<string, IDataType>();
        private readonly Dictionary<string, Module> _modules = new Dictionary<string, Module>();
        private readonly Dictionary<string, Instruction> _instructions = new Dictionary<string, Instruction>();
        private readonly Dictionary<string, Tag> _tags = new Dictionary<string, Tag>();
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


            _components.Add(typeof(DataType), _dataTypes);
        }

        private Controller(XElement element)
        {
            Name = element.Attribute(nameof(Name))?.Value;
            Description = element.Attribute(nameof(Description))?.Value;
            ProcessorType = element.Attribute(nameof(ProcessorType))?.Value;
            MajorRev = Convert.ToUInt64(element.Attribute(nameof(MajorRev))?.Value);
            MinorRev = Convert.ToUInt16(element.Attribute(nameof(MinorRev))?.Value);
            ProjectCreationDate = Convert.ToDateTime(element.Attribute(nameof(ProjectCreationDate))?.Value);
            LastModifiedDate = Convert.ToDateTime(element.Attribute(nameof(LastModifiedDate))?.Value);


            ParseDataTypes(element);
            //instructions
            //modules

            ParseTasks(element);
            ParsePrograms(element);
            //...
        }

        public string Name
        {
            get => _name;
            set
            {
                Validate.Name(value);
                _name = value;
            }
        }

        public Use Use { get; set; }

        public string Description { get; set; }

        public string ProcessorType { get; }

        public ulong MajorRev { get; }

        public ushort MinorRev { get; }

        public DateTime ProjectCreationDate { get; }

        public DateTime LastModifiedDate { get; }

        public IEnumerable<IDataType> DataTypes => _dataTypes.Values.AsEnumerable();

        public IEnumerable<Module> Modules { get; }

        public IEnumerable<Instruction> Instructions { get; }

        public IEnumerable<Tag> Tags { get; }

        public IEnumerable<Program> Programs => _tasks.Values.SelectMany(t => t.Programs);

        public IEnumerable<Task> Tasks => _tasks.Values.AsEnumerable();

        private bool HasContinuousTask => _tasks.Any(t => t.Value.Type == TaskType.Continuous);

        public IControllerCreator Create()
        {
            return new ControllerCreator(this);
        }
        
        public void Create<TModel, TBuilder>(Func<TBuilder> builderFactory, Action<TBuilder> builderConfig)
            where TModel : INamedComponent
            where TBuilder : IBuilder<TModel>
        {
            var type = typeof(TModel);

            var typedBuilder = builderFactory.Invoke();
            builderConfig.Invoke(typedBuilder);

            var item = typedBuilder.Build();

            if (!(_components[type] is Dictionary<string, TModel> component))
                throw new InvalidOperationException();
            
            if (component.ContainsKey(item.Name))
                throw new NameCollisionException();

            component.Add(item.Name, item);
        }

        public IDataType GetDataType(string name)
        {
            var predefined = Predefined.ParseType(name);
            return predefined ?? DataTypes.SingleOrDefault(t => t.Name == name);
        }

        public void AddDataType(IDataType dataType)
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

        public void AddTask(string name)
        {
            var type = HasContinuousTask ? TaskType.Periodic : TaskType.Continuous;
            var task = new Task(name, type);
            AddTask(task);
        }

        public void AddTask(Task task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));

            if (_tasks.ContainsKey(task.Name))
                Throw.NameCollisionException(task.Name, typeof(Task));

            if (HasContinuousTask && task.Type == TaskType.Continuous)
                throw new InvalidOperationException();

            _tasks.Add(task.Name, task);
        }

        public void RemoveTask(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (!_tasks.ContainsKey(name))
                Throw.ItemNotFoundException(name);

            _tasks.Remove(name);
        }
        
        public void RenameTask(string oldName, string newName)
        {
            if (oldName == null)
                throw new ArgumentNullException(nameof(oldName));

            if (!_tasks.ContainsKey(oldName))
                Throw.ItemNotFoundException(oldName);

            var current = _tasks[oldName];
            _tasks.Remove(oldName);

            current.Name = newName;
            _tasks.Add(current.Name, current);
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

            element.Add(new XElement(nameof(DataTypes)), _dataTypes.Values.Select(d => ((DataType)d).Serialize()));
            //module
            //instructions
            //tags
            element.Add(new XElement(nameof(Programs)),
                _tasks.Values.SelectMany(t => t.Programs.Select(p => p.Serialize())));
            element.Add(new XElement(nameof(Tasks)), _tasks.Values.Select(t => t.Serialize()));

            return element;
        }

        public static Controller Materialize(XElement element)
        {
            return new Controller(element);
        }

        private void ParseDataTypes(XContainer element)
        {
            var dataTypes = element.Element(nameof(DataTypes))?.Descendants().Select(DataType.Materialize);
            if (dataTypes == null) return;
            foreach (var dataType in dataTypes)
                _dataTypes.Add(dataType.Name, dataType);
        }

        private void ParseTasks(XContainer element)
        {
            var tasks = element.Element(nameof(Tasks))?.Descendants().Select(Task.Materialize);
            if (tasks == null) return;
            foreach (var task in tasks)
                _tasks.Add(task.Name, task);
        }

        private void ParsePrograms(XContainer element)
        {
            var programs = element.Element(nameof(Programs))?.Descendants().Select(Program.Materialize);
            if (programs == null) return;
            foreach (var program in programs)
            {
                var taskName = element.Descendants("Task")
                    .SingleOrDefault(t => t.Descendants("ScheduledProgram")
                        .SingleOrDefault(p => p.Attribute("Name")?.Value == program.Name) != null)
                    ?.Attribute("Name")?.Value;

                if (taskName == null)
                    throw new InvalidOperationException();

                var task = _tasks[taskName];

                task.AddProgram(program);
            }
        }
    }
}