using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class ArrayMemberSerializer : IXSerializer<IMember<IDataType>>
    {
        private const string ElementName = LogixNames.ArrayMember;

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            if (!(component is IArrayMember<IDataType> arrayMember))
                throw new InvalidOperationException("ArrayMembers must be of type IArrayMember.");
            
            var element = new XElement(ElementName);
            
            element.Add(component.ToAttribute(m => m.Name));
            element.Add(component.ToAttribute(m => m.DataType));
            element.Add(component.ToAttribute(m => m.Dimension));
            element.Add(component.ToAttribute(m => m.Radix));

            var serializer = new ArrayElementSerializer();
            var elements = arrayMember.Select(m => serializer.Serialize(m));
            element.Add(elements);

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetName();
            var dimensions = element.GetValue<IArrayMember<IDataType>, Dimensions>(m => m.Dimension);
            var radix = element.GetValue<IArrayMember<IDataType>, Radix>(m => m.Radix);
            
            var serializer = new ArrayElementSerializer();
            var members = element.Elements().Select(e => serializer.Deserialize(e)).ToArray();

            return Member.Array(name, members[0].DataType, dimensions, radix, members: members);
        }
    }
}