using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Controller : LogixComponent, IController
    {
        private readonly Dictionary<string, IDataType> _dataTypes = new Dictionary<string, IDataType>();
        private readonly Dictionary<string, ITag> _tags = new Dictionary<string, ITag>();
        private readonly Dictionary<string, IProgram> _programs = new Dictionary<string, IProgram>();
        private readonly Dictionary<string, ITask> _tasks = new Dictionary<string, ITask>();

        public Controller(string name, Revision revision = null, ProcessorType processorType = null,
            string description = null) : base(name, description)
        {
            ProcessorType = processorType;
            Revision = revision;
            ProjectCreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }

        public ProcessorType ProcessorType { get; }

        public Revision Revision { get; set; }

        public int MajorRev => Revision.Major;

        public int MinorRev => Revision.Minor;

        public DateTime ProjectCreationDate { get; }

        public DateTime LastModifiedDate { get; }

        public IEnumerable<IDataType> DataTypes => _dataTypes.Values.AsEnumerable();
        public IEnumerable<ITag> Tags => _tags.Values.AsEnumerable();
        public IEnumerable<IProgram> Programs => _programs.Values.AsEnumerable();
        public IEnumerable<ITask> Tasks => _tasks.Values.AsEnumerable();

        public void AddDataType(IDataType dataType)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));

            if (_dataTypes.ContainsKey(dataType.Name))
                Throw.ComponentNameCollisionException(dataType.Name, typeof(DataType));

            _dataTypes.Add(dataType.Name, dataType);
        }

        public void RemoveDataType(IDataType dataType)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));

            if (_tags.Values.Any(t => t.DataType.Equals(dataType)))
                Throw.ComponentReferencedException(dataType.Name, typeof(DataType));

            if (!_dataTypes.ContainsKey(dataType.Name)) return;

            _dataTypes.Remove(dataType.Name);
        }

        public void AddTag(ITag tag)
        {
            if (tag == null) throw new ArgumentNullException(nameof(tag));

            if (_tags.ContainsKey(tag.Name))
                Throw.ComponentNameCollisionException(tag.Name, typeof(Tag));

            _tags.Add(tag.Name, tag);
        }

        public void RemoveTag(ITag tag)
        {
            if (tag == null) throw new ArgumentNullException(nameof(tag));

            //todo I guess tags that are referenced in rungs cant be deleted. Should we enforce that?
            /*if (_tags.Values.Any(x => x.DataType == dataType.Name))
                Throw.ComponentReferencedException(dataType.Name, typeof(DataType));*/

            if (!_tags.ContainsKey(tag.Name))
                Throw.ComponentNotFoundException(tag.Name, typeof(Tag));

            _tags.Remove(tag.Name);
        }

        public void AddProgram(IProgram program)
        {
            throw new NotImplementedException();
        }

        public void RemoveProgram(IProgram program)
        {
            throw new NotImplementedException();
        }

        public void AddTask(ITask task)
        {
            throw new NotImplementedException();
        }

        public void RemoveTask(ITask task)
        {
            throw new NotImplementedException();
        }
    }
}