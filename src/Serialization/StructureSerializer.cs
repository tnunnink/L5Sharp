using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class StructureSerializer : IXSerializer<IComplexType>
    {
        private const string ElementName = LogixNames.Structure;

        public XElement Serialize(IComplexType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(component.ToAttribute(c => c.Name, LogixNames.DataType));

            var elements = component.Members.Select(m => m.Serialize(m.GetDataElementName()));
            element.Add(elements);

            return element;
        }

        public IComplexType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName || element.Name == LogixNames.StructureMember)
                throw new ArgumentException(
                    $"Element name '{element.Name}' invalid. Expecting '{ElementName}' or {LogixNames.StructureMember}");

            var name = element.GetDataTypeName();
            var members = element.Elements().Select(e => e.Deserialize<IMember<IDataType>>());

            return new DataType(name, DataTypeClass.Unknown, string.Empty, members);
        }
    }
}