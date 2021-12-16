using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
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

            var element = new XElement(ElementName);
            
            element.Add(component.ToAttribute(m => m.Name));
            element.Add(component.ToAttribute(m => m.DataType));
            element.Add(component.ToAttribute(m => m.Dimensions));
            element.Add(component.ToAttribute(m => m.Radix));

            var serializer = new ArrayElementSerializer();
            var elements = component.Select(m => serializer.Serialize(m));
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
            var dimensions = element.GetValue<IMember<IDataType>, Dimensions>(m => m.Dimensions);
            var radix = element.GetValue<IMember<IDataType>, Radix>(m => m.Radix);
            
            var serializer = new ArrayElementSerializer();
            var members = element.Elements().Select(e => serializer.Deserialize(e)).ToArray();

            throw new NotImplementedException();
            /*return ArrayMember.Create(name, members, dimensions, radix);*/
        }
    }
}