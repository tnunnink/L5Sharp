using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class MemberSerializer : IXSerializer<IMember<IDataType>>
    {
        private readonly LogixContext _context;

        public MemberSerializer(LogixContext context)
        {
            _context = context;
        }
        
        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));
            
            var element = new XElement(LogixNames.Member);
            element.Add(component.ToAttribute(c => c.Name));
            element.Add(component.ToAttribute(c => c.DataType));
            element.Add(component.ToAttribute(c => c.Dimension));
            element.Add(component.ToAttribute(c => c.Radix));
            element.Add(component.ToAttribute(c => c.ExternalAccess));
            
            if (!string.IsNullOrEmpty(component.Description))
                element.Add(component.ToElement(x => x.Description));

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null) return null;

            var name = element.GetName();
            var dataType = _context.TypeRegistry.TryGetType(element.GetDataTypeName());
            var description = element.GetValue<IMember<IDataType>, string>(m => m.Description);
            var dimensions = element.GetValue<IMember<IDataType>, Dimensions>(m => m.Dimension);
            var radix = element.GetValue<IMember<IDataType>, Radix>(m => m.Radix);
            var access = element.GetValue<IMember<IDataType>, ExternalAccess>(m => m.ExternalAccess);

            return Member.Create(name, dataType, dimensions, radix, access, description);
        }
    }
}