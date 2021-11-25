using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class TagSerializer : IXSerializer<ITag<IDataType>>
    {
        public const string Data = nameof(Data);
        public const string DataValue = nameof(DataValue);
        public const string Array = nameof(Array);
        public const string Index = nameof(Index);
        public const string Element = nameof(Element);
        public const string Structure = nameof(Structure);
        public const string ArrayMember = nameof(ArrayMember);
        public const string DataValueMember = nameof(DataValueMember);
        public const string StructureMember = nameof(StructureMember);
        
        private readonly LogixContext _context;

        public TagSerializer(LogixContext context)
        {
            _context = context;
        }
        
        public XElement Serialize(ITag<IDataType> component)
        {
            throw new NotImplementedException();
        }

        public ITag<IDataType> Deserialize(XElement element)
        {
            if (element == null) return null;

            var name = element.GetName();
            var dataType = _context.TypeRegistry.TryGetType(element.GetDataTypeName())?.Instantiate();
            var dimensions = element.GetValue<Tag<IDataType>, Dimensions>(t => t.Dimensions);
            var radix = element.GetValue<Tag<IDataType>, Radix>(t => t.Radix);
            var access = element.GetValue<Tag<IDataType>, ExternalAccess>(t => t.ExternalAccess);
            var usage = element.GetValue<Tag<IDataType>, TagUsage>(m => m.Usage);
            var constant = element.GetValue<Tag<IDataType>, bool>(m => m.Constant);
            var description = element.GetValue<Tag<IDataType>, string>(m => m.Description);

            return new Tag<IDataType>(name, dataType, dimensions, radix, access, description, usage, constant);
        }
    }
}