using System;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Types;

namespace L5Sharp
{
    internal class LogixTypeRegistryItem : IEquatable<LogixTypeRegistryItem>, IInstantiable<IDataType>
    {
        public LogixTypeRegistryItem(string name, DataTypeClass @class, XElement element, 
            Func<XElement, IDataType> factory)
        {
            Name = name;
            Class = @class;
            Element = element;
            Factory = factory;
        }

        public string Name { get; }
        private DataTypeClass Class { get; }
        private Func<XElement, IDataType> Factory { get; }
        private XElement Element { get; }

        public IDataType Instantiate()
        {
            return Factory.Invoke(Element);
        }

        public bool Equals(LogixTypeRegistryItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(Class, other.Class);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((LogixTypeRegistryItem)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Class);
        }

        public static bool operator ==(LogixTypeRegistryItem left, LogixTypeRegistryItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LogixTypeRegistryItem left, LogixTypeRegistryItem right)
        {
            return !Equals(left, right);
        }
    }
}