using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class ArrayMemberSerializer : IXSerializer<IArrayMember<IDataType>>
    {
        private const string ElementName = LogixNames.ArrayMember;
        
        public XElement Serialize(IArrayMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            var element = new XElement(ElementName);
            
            element.Add(component.ToAttribute(m => m.Name));
            element.Add(component.ToAttribute(m => m.DataType));
            element.Add(component.ToAttribute(m => m.Dimension));
            element.Add(component.ToAttribute(m => m.Radix));

            //todo element serializer?

            return element;
        }

        public IArrayMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetName();
            //todo get data type
            var dimensions = element.GetValue<IArrayMember<IDataType>, Dimensions>(m => m.Dimension);
            var radix = element.GetValue<IArrayMember<IDataType>, Radix>(m => m.Radix);
            
            //todo element serializer

            return Member.Array(name, null, dimensions, radix);
        }
    }
}