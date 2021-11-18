using System;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Types;

namespace L5Sharp
{
    internal class LogixTypeRegistryItem : IEquatable<LogixTypeRegistryItem>, IInstantiable<IDataType>
    {
        public LogixTypeRegistryItem(string name, DataTypeClass @class, XElement element, IComponentMaterializer materializer)
        {
            Name = name;
            Class = @class;
            Element = element;
            Materializer = materializer;
        }

        public string Name { get; }
        private DataTypeClass Class { get; }
        private IComponentMaterializer Materializer { get; }
        private XElement Element { get; }

        public IDataType Instantiate()
        {
            if (Class == DataTypeClass.User)
            {
                return ((UserDefinedMaterializer)Materializer).Materialize(Element);
            }

            if (Class == DataTypeClass.AddOnDefined)
            {
                //return ((AddOnDefinedFactory)Factory).Create(Element);
            }

            return new Undefined();
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