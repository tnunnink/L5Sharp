using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Builders.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Controller : IComponent
    {
        private string _name;
        private readonly Dictionary<string, DataType> _dataTypes = new Dictionary<string, DataType>();
        private readonly Dictionary<string, Tag> _tags = new Dictionary<string, Tag>();

        public Controller(string name, string description = null, string processorType = null, Revision revision = null)
        {
            Name = name;
            Use = Use.Target;
            Description = description ?? string.Empty;
            ProcessorType = processorType ?? string.Empty;
            Revision = revision ?? Revision.v32;
            ProjectCreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
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

        public Revision Revision { get; set; }

        public int MajorRev => Revision.Major;

        public int MinorRev => Revision.Minor;

        public DateTime ProjectCreationDate { get; }

        public DateTime LastModifiedDate { get; }

        public IEnumerable<DataType> DataTypes => _dataTypes.Values.AsEnumerable();
        public IEnumerable<Tag> Tags => _tags.Values.AsEnumerable();

        public void AddDataType(DataType dataType)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));

            if (_dataTypes.ContainsKey(dataType.Name))
                Throw.ComponentNameCollisionException(dataType.Name, typeof(DataType));

            _dataTypes.Add(dataType.Name, dataType);
        }
        
        public void RemoveDataType(DataType dataType)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));
            
            if (_tags.Values.Any(x => x.DataType == dataType.Name))
                Throw.ComponentReferencedException(dataType.Name, typeof(DataType));

            if (!_dataTypes.ContainsKey(dataType.Name))
                Throw.ComponentNotFoundException(dataType.Name, typeof(DataType));

            _dataTypes.Remove(dataType.Name);
        }
        
        public void AddTag(Tag tag)
        {
            if (tag == null) throw new ArgumentNullException(nameof(tag));

            if (_tags.ContainsKey(tag.Name))
                Throw.ComponentNameCollisionException(tag.Name, typeof(Tag));

            _tags.Add(tag.Name, tag);
        }
        
        public void RemoveTag(Tag tag)
        {
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            
            //todo I guess tags that are referenced in rung cant be deleted. Should we enforce that?
            /*if (_tags.Values.Any(x => x.DataType == dataType.Name))
                Throw.ComponentReferencedException(dataType.Name, typeof(DataType));*/
            
            if (!_tags.ContainsKey(tag.Name))
                Throw.ComponentNotFoundException(tag.Name, typeof(Tag)); 

            _tags.Remove(tag.Name);
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
        
    }
}