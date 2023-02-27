using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="StructureType"/> or structure data elements.
    /// </summary>
    public class StructureSerializer : ILogixSerializer<StructureType>
    {
        private readonly MemberSerializer _memberSerializer = new();

        /// <inheritdoc />
        public XElement Serialize(StructureType obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Structure);
            element.AddValue(obj.Name, L5XName.DataType);
            element.Add(obj.Members.Select(m => _memberSerializer.Serialize(m)));

            return element;
        }

        /// <inheritdoc />
        public StructureType Deserialize(XElement element)
        {
            Check.NotNull(element);

            var dataType = element.GetValue<string>(L5XName.DataType);
            var members = element.Elements().Select(e => _memberSerializer.Deserialize(e));

            return new StructureType(dataType, members);
        }
    }
}