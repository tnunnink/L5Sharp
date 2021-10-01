using System.Collections.Generic;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Abstractions
{
    public interface ITMember : INamedComponent
    {
        public string DataType { get; set; }
        public Dimensions Dimensions { get; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; }
        object Value { get; }
        public IEnumerable<ITMember> Members { get; }
        bool IsValueMember { get; }
        bool IsArrayMember { get; }
        bool IsArrayElement { get; }
        bool IsStructureMember { get; }
    }
    
    public interface ITMember<out T> : ITMember where T : IDataType, new()
    {
    }

    public class TMember : ITMember
    {
        public string Name { get; }
        public string DataType { get; set; }
        public Dimensions Dimensions { get; }
        public Radix Radix { get; set; }
        public ExternalAccess ExternalAccess { get; internal set; }
        public object Value { get; set; }
        public IEnumerable<ITMember> Members { get; }
        public bool IsValueMember { get; }
        public bool IsArrayMember { get; }
        public bool IsArrayElement { get; }
        public bool IsStructureMember { get; }
    }

    public class Tg : ITMember
    {
        public Tg(string name, IDataType dataType)
        {
        }
        
        public string Name { get; }
        public string DataType { get; set; }
        public Dimensions Dimensions { get; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; }
        public object Value { get; }
        public IEnumerable<ITMember> Members { get; }
        public bool IsValueMember { get; }
        public bool IsArrayMember { get; }
        public bool IsArrayElement { get; }
        public bool IsStructureMember { get; }
    }

    public class Tg<T> : ITMember<T> where T : IDataType, new()
    {
        private readonly T _dataType;

        public Tg()
        {
            _dataType = new T();
        }
        
        public string Name { get; }
        public string DataType { get; set; }
        public Dimensions Dimensions { get; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; }
        public object Value { get; }
        public IEnumerable<ITMember> Members { get; }
        public bool IsValueMember { get; }
        public bool IsArrayMember { get; }
        public bool IsArrayElement { get; }
        public bool IsStructureMember { get; }
    }


}