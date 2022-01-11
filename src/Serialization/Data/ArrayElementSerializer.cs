using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization.Data
{
    internal class ArrayElementSerializer : IXSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = LogixNames.Element;
        
        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(new XAttribute(LogixNames.Index, component.Name));

            switch (component.DataType)
            {
                case IAtomicType atomic:
                    element.Add(new XAttribute(LogixNames.Value, atomic.Format(component.Radix)));
                    break;
                case IComplexType complexType:
                {
                    var serializer = new StructureSerializer();
                    var structure = serializer.Serialize(complexType);
                    element.Add(structure);
                    break;
                }
            }

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var index = element.Attribute(LogixNames.Index)?.Value;
            var dataType = element.GetDataType();

            return new Member<IDataType>(index!, dataType);
        }
    }
}