using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Factories.Tests")]

namespace L5Sharp.Factories
{
    internal class MemberFactory : IComponentFactory<IMember<IDataType>>
    {
        private readonly LogixContext _context;

        public MemberFactory(LogixContext context)
        {
            _context = context;
        }

        public IMember<IDataType> Create(XElement element)
        {
            if (element == null) return null;

            var name = element.GetName();
            var dataType = _context.TypeRegistry.TryGetType(element.GetDataTypeName());
            var description = element.GetDescription();
            var dimensions = element.GetValue<Member<IDataType>>(m => m.Dimensions);
            var radix = element.GetValue<Member<IDataType>>(m => m.Radix);
            var access = element.GetValue<Member<IDataType>>(m => m.ExternalAccess);

            return Member.Create(name, dataType, dimensions, radix, access, description);
        }
    }
}