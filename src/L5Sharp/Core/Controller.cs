using System;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Builders;
using L5Sharp.Builders.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Repositories;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Controller : IController
    {
        private readonly Logix _logix;
        private string _name;

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

            _logix = new Logix(string.Empty); //todo actually load or create here

            DataTypes = new DataTypeRepository(_logix);
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
        
        public IDataTypeRepository DataTypes { get; }

        public IControllerCreator Create()
        {
            return new ControllerCreator(this);
        }

        public void Build<TModel, TBuilder>(Func<TBuilder> builderFactory, Action<TBuilder> builderConfig)
            where TModel : IComponent
            where TBuilder : IBuilder<TModel>
        {
            var type = typeof(TModel);

            var typedBuilder = builderFactory.Invoke();
            builderConfig.Invoke(typedBuilder);

            var item = typedBuilder.Build();

            /*if (!(_collections[type] is Dictionary<string, TModel> collection))
                throw new InvalidOperationException();
            
            if (collection.ContainsKey(item.Name))
                throw new NameCollisionException();

            collection.Add(item.Name, item);*/
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

            /*element.Add(new XElement(nameof(DataTypes)), _dataTypes.Values.Select(d => ((DataType)d).Serialize()));
            //module
            //instructions
            //tags
            element.Add(new XElement(nameof(Programs)),
                _tasks.Values.SelectMany(t => t.Programs.Select(p => p.Serialize())));
            element.Add(new XElement(nameof(Tasks)), _tasks.Values.Select(t => t.Serialize()));*/

            return element;
        }

        /*private Dictionary<string, T> GetCollection<T>(Type type) where T : INamedComponent
        {
            if (!_collections.ContainsKey(type))
                throw new InvalidOperationException($"There is not collection for the specified type '{type}'");

            if (!(_collections[type] is Dictionary<string, T> collection))
                throw new InvalidOperationException(
                    $"The collection found for type '{type}' is not able to be casted to the generic dictionary");

            return collection;
        }*/

        /*private void ParsePrograms(XContainer element)
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
        }*/
    }
}