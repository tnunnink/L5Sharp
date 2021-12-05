using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class UserDefinedMemberSerializer : IXSerializer<IMember<IDataType>>
    {
        private const string ElementName = LogixNames.Member;

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.Add(component.ToAttribute(c => c.Name));
            element.Add(component.ToAttribute(c => c.DataType));
            element.Add(component.ToAttribute(c => c.Dimensions));
            element.Add(component.ToAttribute(c => c.Radix));
            element.Add(component.ToAttribute(c => c.ExternalAccess));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(component.ToElement(x => x.Description));

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetName() ?? throw new ArgumentNullException();
            var dataType = new LogixTypeProvider(element.Document?.Root).GetDataType(element.GetDataTypeName());
            var dimensions = element.GetValue<IMember<IDataType>, Dimensions>(m => m.Dimensions);
            var radix = element.GetValue<IMember<IDataType>, Radix>(m => m.Radix);
            var access = element.GetValue<IMember<IDataType>, ExternalAccess>(m => m.ExternalAccess);
            var description = element.GetValue<IMember<IDataType>, string>(m => m.Description);

            return Member.Create(name, dataType, dimensions, radix, access, description);
        }
    }
}