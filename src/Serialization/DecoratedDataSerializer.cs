using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    internal class DecoratedDataSerializer : IXSerializer<IDataType>
    {
        private static readonly XName ElementName = LogixNames.Data;

        public XElement Serialize(IDataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            switch (component)
            {
                case IAtomicType atomicType:
                    var dataValueSerializer = new DataValueSerializer();
                    element.Add(dataValueSerializer.Serialize(atomicType));
                    break;
                case IArrayType<IDataType> arrayType:
                    var arraySerializer = new ArraySerializer();
                    element.Add(arraySerializer.Serialize(arrayType));
                    break;
                case IComplexType complexType:
                    var structureSerializer = new StructureSerializer();
                    element.Add(structureSerializer.Serialize(complexType));
                    break;
            }

            return element;
        }

        public IDataType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException(
                    $"Element name '{element.Name}' invalid. Expecting '{ElementName}'.");

            var root = element.Elements().First();

            if (root.Name == LogixNames.DataValue)
            {
                var atomic = (IAtomicType)DataType.Create(root.GetDataTypeName());
                var value = root.Attribute(LogixNames.Value)?.Value;
                atomic.SetValue(value!);
                return atomic;
            }

            if (root.Name == LogixNames.Array)
            {
                var arraySerializer = new ArraySerializer();
                return arraySerializer.Deserialize(root);
            }

            if (root.Name != LogixNames.Structure)
                throw new NotSupportedException(
                    $"The provided element {ElementName} data member with name {root.Name} is not supported for serialization");
            
            var structureSerializer = new StructureSerializer();
            return structureSerializer.Deserialize(root);
        }
    }
}