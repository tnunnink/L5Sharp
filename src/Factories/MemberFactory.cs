using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
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
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            
            var typeName = element.GetDataTypeName();
            var dataType = _context.DataTypes.Get(typeName);

            var name = element.GetName();
            var description = element.GetDescription();
            var dimensions = element.GetValue<IMember<IDataType>>(m => m.Dimensions);
            var radix = element.GetValue<IMember<IDataType>>(m => m.Radix);
            var access = element.GetValue<IMember<IDataType>>(m => m.ExternalAccess);

            return Member.Create(name, (IDataType)dataType, dimensions, radix, access, description);
        }
    }
}