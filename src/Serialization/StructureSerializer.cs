using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization
{
    internal class StructureSerializer : IXSerializer<IComplexType>
    {
        private static readonly XName ElementName = LogixNames.Structure;

        public XElement Serialize(IComplexType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddAttribute(component, c => c.Name, nameOverride: LogixNames.DataType);

            var members = component.Members.Select(m => m.Serialize());
            element.Add(members);

            return element;
        }

        public IComplexType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName || element.Name == LogixNames.StructureMember)
                throw new ArgumentException(
                    $"Element name '{element.Name}' invalid. Expecting '{ElementName}' or {LogixNames.StructureMember}");

            var name = element.Attribute(LogixNames.DataType)?.Value;
            var members = element.Elements().Select(e => e.Deserialize<IMember<IDataType>>(e.Name.ToString()));

            //can we get class based on parent?
            //Module = IO
            //AOI = AddOnDefined
            //Otherwise = Unknown?
            return new StructureType(name!, DataTypeClass.Unknown, members);
        }
    }
}