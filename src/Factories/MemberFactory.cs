using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Factories.Tests")]

namespace L5Sharp.Factories
{
    internal class MemberFactory : IComponentFactory<IMember>
    {
        private readonly LogixContext _context;

        public MemberFactory(LogixContext context)
        {
            _context = context;
        }

        public IMember Create(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            
            var typeName = element.GetDataTypeName();
            var dataType = _context.DataTypes.Get(typeName);

            var name = element.GetName();
            var description = element.GetDescription();
            var dimensions = element.GetValue<IMember>(m => m.Dimensions);
            var radix = element.GetValue<IMember>(m => m.Radix);
            var access = element.GetValue<IMember>(m => m.ExternalAccess);

            return new DataTypeMember(name, dataType, dimensions, radix, access, description);
        }
    }
}