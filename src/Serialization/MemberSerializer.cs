using System;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    internal class MemberSerializer : IXSerializer<IMember<IDataType>>
    {
        private readonly LogixContext _context;
        private static readonly XName ElementName = LogixNames.Member;

        public MemberSerializer(LogixContext context)
        {
            _context = context;
        }

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.DataType.Name, nameOverride: nameof(component.DataType));
            element.AddAttribute(component, c => c.Dimensions);
            element.AddAttribute(component, c => c.Radix);
            element.Add(new XAttribute("Hidden", false));
            element.AddAttribute(component, c => c.ExternalAccess);

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetComponentName();
            var description = element.GetComponentDescription();
            var dataType = _context.Types.GetDataType(element.GetDataTypeName());
            var dimensions = element.GetAttribute<Member<IDataType>, Dimensions>(m => m.Dimensions);
            var radix = element.GetAttribute<Member<IDataType>, Radix>(m => m.Radix);
            var access = element.GetAttribute<Member<IDataType>, ExternalAccess>(m => m.ExternalAccess);

            if (dimensions is null || dimensions.AreEmpty)
                return new Member<IDataType>(name, dataType, radix, access, description);
            
            var arrayType = new ArrayType<IDataType>(dimensions, dataType, radix, access, description);
            return new Member<IDataType>(name, arrayType, radix, access, description);
        }
    }
}