using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Controller : IComponent
    {
        private string _name;
        private readonly Dictionary<string, DataType> _dataTypes = new Dictionary<string, DataType>();
        private readonly Dictionary<string, Tag> _tags = new Dictionary<string, Tag>();

        public Controller(string name, Revision revision = null, ProcessorType processorType = null, string description = null)
        {
            Name = name;
            Use = Use.Target;
            Description = description ?? string.Empty;
            ProcessorType = processorType;
            Revision = revision;
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

        public ProcessorType ProcessorType { get; }

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

    }
}