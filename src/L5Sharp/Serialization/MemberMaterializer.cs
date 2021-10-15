using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class MemberMaterializer : IComponentMaterializer<IMember>
    {
        private readonly LogixContext _context;

        public MemberMaterializer(LogixContext context)
        {
            _context = context;
        }

        public IMember Materialize(XElement element)
        {
            var typeName = element.GetValue<IMember, IDataType, string>(m => m.DataType, s => s);
            var dataType = _context.FindDataType(typeName);

            var name = element.GetName();
            var description = element.GetDescription();
            var dimensions = element.GetValue<IMember>(m => m.Dimension);
            var radix = element.GetValue<IMember>(m => m.Radix);
            var access = element.GetValue<IMember>(m => m.ExternalAccess);

            return new Member(name, dataType, dimensions, radix, access, description);
        }
    }
}