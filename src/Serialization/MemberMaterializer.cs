using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class MemberMaterializer : IComponentMaterializer<IMember<IDataType>>
    {
        private readonly LogixContext _context;

        public MemberMaterializer(LogixContext context)
        {
            _context = context;
        }

        public IMember<IDataType> Materialize(XElement element)
        {
            if (element == null) return null;

            var name = element.GetName();
            var dataType = _context.TypeRegistry.TryGetType(element.GetDataTypeName());
            var description = element.GetDescription();
            var dimensions = element.GetValue<Member<IDataType>>(m => m.Dimension);
            var radix = element.GetValue<Member<IDataType>>(m => m.Radix);
            var access = element.GetValue<Member<IDataType>>(m => m.ExternalAccess);

            return Member.Create(name, dataType, dimensions, radix, access, description);
        }
    }
}