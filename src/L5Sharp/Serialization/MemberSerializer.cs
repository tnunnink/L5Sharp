using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Loaders.Tests")]

namespace L5Sharp.Serialization
{
    internal class MemberSerializer : IL5XSerializer<Member>
    {
        private readonly DataTypeSerializer _typeSerializer = new DataTypeSerializer();
        
        public XElement Serialize(Member component)
        {
            var element = new XElement(nameof(Member));
            element.Add(new XAttribute(nameof(component.Name), component.Name));
            element.Add(new XAttribute(nameof(component.DataType), component.DataType.Name));
            element.Add(new XAttribute(nameof(component.Dimension), component.Dimension));
            element.Add(new XAttribute(nameof(component.Radix), component.Radix));
            element.Add(new XAttribute(nameof(component.Hidden), component.Hidden));
            if (!string.IsNullOrEmpty(component.Target))
                element.Add(new XAttribute(nameof(component.Target), component.Target));
            if (!string.IsNullOrEmpty(component.Target))
                element.Add(new XAttribute(nameof(component.BitNumber), component.BitNumber));
            element.Add(new XAttribute(nameof(component.ExternalAccess), component.ExternalAccess));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(new XElement(nameof(component.Description), new XCData(component.Description)));

            return element;
        }

        public Member Deserialize(XElement element)
        {
            var typeName = element.GetDataTypeName();
            
            var dataType = Predefined.ContainsType(typeName)
                ? (IDataType) Predefined.FromName(typeName)
                : _typeSerializer.Deserialize(element.FindDataType(typeName));
            
            return new Member(element.GetName(), dataType, element.GetDimension(), element.GetRadix(),
                element.GetExternalAccess(), element.GetDescription(), element.GetHidden(), element.GetTarget(),
                element.GetBitNumber());
        }
    }
}