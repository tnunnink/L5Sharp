using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="DataType"/> components.
    /// </summary>
    public class DataTypeSerializer : ILogixSerializer<DataType>
    {
        private readonly DataTypeMemberSerializer _memberSerializer = new();
        
        /// <inheritdoc />
        public XElement Serialize(DataType obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.DataType);
            
            element.AddValue(obj, d => d.Name);
            element.AddText(obj, d => d.Description);
            element.AddValue(obj.Family.Value, L5XName.Family);
            element.AddValue(obj.Class.Value, L5XName.Class);
            
            var members = new XElement(nameof(L5XName.Members));
            members.Add(obj.Members.Select(m => _memberSerializer.Serialize(m)));
            element.Add(members);

            return element;
        }

        /// <inheritdoc />
        public DataType Deserialize(XElement element)
        {
            Check.NotNull(element);

            var members = element.Descendants(L5XName.Member)
                .Where(e => e.Attribute(L5XName.Hidden) is null 
                            || bool.Parse(e.Attribute(L5XName.Hidden)!.Value) == false)
                .Select(m => _memberSerializer.Deserialize(m));
            
            return new DataType
            {
                Name = element.LogixName(),
                Description = element.LogixDescription(),
                Family = element.GetValue<DataTypeFamily>(L5XName.Family),
                Class = element.GetValue<DataTypeClass>(L5XName.Class),
                Members = members.ToList()
            };
        }
    }
}