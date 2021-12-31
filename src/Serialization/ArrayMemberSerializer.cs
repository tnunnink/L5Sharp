using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization
{
    internal class ArrayMemberSerializer : IXSerializer<IMember<IDataType>>
    {
        private static readonly XName ElementName = LogixNames.ArrayMember;

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddValue(component, m => m.Name);
            element.AddValue(component, m => m.DataType);
            element.AddValue(component, m => m.Dimension);
            element.AddValue(component, m => m.Radix);

            /*var serializer = new ArrayElementSerializer();
            var elements = component.Select(m => serializer.Serialize(m));
            element.Add(elements);*/

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetComponentName();
            var dimensions = element.GetValue<IMember<IDataType>, Dimensions>(m => m.Dimension);
            var radix = element.GetValue<IMember<IDataType>, Radix>(m => m.Radix);
            
            /*var serializer = new ArrayElementSerializer();
            var members = element.Elements().Select(e => serializer.Deserialize(e)).ToArray();*/

            throw new NotImplementedException();
            /*return ArrayMember.Create(name, members, dimensions, radix);*/
        }
    }
}