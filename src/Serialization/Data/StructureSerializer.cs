using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="StructureType"/> or structure data elements.
    /// </summary>
    public class StructureSerializer : ILogixSerializer<StructureType>
    {
        /// <inheritdoc />
        public XElement Serialize(StructureType obj)
        {
            Check.NotNull(obj);

            //String structures are kinda unique for some reason, so we are handling it here.
            if (obj is StringType stringType)
                return TagDataSerializer.StringStructure.Serialize(stringType);

            var element = new XElement(L5XName.Structure);
            element.AddValue(obj.Name, L5XName.DataType);
            element.Add(obj.Members.Select(m => TagDataSerializer.Member.Serialize(m)));

            return element;
        }

        /// <inheritdoc />
        public StructureType Deserialize(XElement element)
        {
            Check.NotNull(element);
            
            if (element.HasLogixStringStructure())
                return TagDataSerializer.StringStructure.Deserialize(element);

            var dataType = element.GetValue<string>(L5XName.DataType);
            var members = element.Elements().Select(e => TagDataSerializer.Member.Deserialize(e));

            return new StructureType(dataType, members);
        }
    }
}