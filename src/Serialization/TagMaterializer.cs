using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class TagMaterializer : IComponentMaterializer<ITag<IDataType>>
    {
        private readonly LogixContext _context;

        public TagMaterializer(LogixContext context)
        {
            _context = context;
        }

        public ITag<IDataType> Materialize(XElement element)
        {
            if (element == null) return null;

            var name = element.GetName();
            var dataType = _context.TypeRegistry.TryGetType(element.GetDataTypeName())?.Instantiate();
            var dimensions = element.GetValue<Tag<IDataType>>(t => t.Dimensions);
            var radix = element.GetValue<Tag<IDataType>>(t => t.Radix);
            var access = element.GetValue<Tag<IDataType>>(t => t.ExternalAccess);
            var usage = element.GetValue<Tag<IDataType>>(m => m.Usage);
            var constant = element.GetValue<Tag<IDataType>>(m => m.Constant);
            var description = element.GetDescription();

            return new Tag<IDataType>(name, dataType, dimensions, radix, access, description, usage, constant);
        }
    }
}