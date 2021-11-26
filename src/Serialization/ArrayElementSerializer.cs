using System;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class ArrayElementSerializer : IXSerializer<IMember<IDataType>>
    {
        private const string ElementName = LogixNames.Element;
        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            var element = new XElement(LogixNames.Element);
            
            element.Add(component.ToAttribute(x => x.Name, LogixNames.Index));

            if (component.DataType is IAtomic atomic)
                element.Add(atomic.ToAttribute(x => x.Value));

            if (component.DataType is IComplexType complexType)
            {
                //we need to add a structure here i think...
            }

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetName();
            //todo get data type. This may be on the parent..
            //todo if the type is atomic it will have a value we can set.

            return Member.Create(name, null);
        }
    }
}