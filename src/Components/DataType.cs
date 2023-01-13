using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Components
{
    /// <summary>
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    public class DataType : ILogixComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;


        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets the value of the <see cref="DataTypeFamily"/> for the <see cref="DataType"/> instance.
        /// </summary>
        /// <value>
        /// An enum value indicating whether the data type belongs to the string family or has no family.
        /// </value>
        public DataTypeFamily Family { get; set; } = DataTypeFamily.None;

        /// <summary>
        /// Gets the value of the <see cref="DataTypeClass"/> for the <see cref="DataType"/> instance.
        /// </summary>
        /// <value>
        /// An enum value indicating the class for which the current data type belongs.
        /// This could be atomic, user, predefined, etc.
        /// </value>
        public DataTypeClass Class => DataTypeClass.User;

        /// <summary>
        /// A collection of <see cref="DataTypeMember"/> objects that together make up the structure of the
        /// <see cref="DataType"/> component.
        /// </summary>
        public List<DataTypeMember> Members { get; set; } = new();
        

        /// <inheritdoc />
        public XElement Serialize()
        {
            var element = new XElement(L5XName.DataType);
            element.AddComponentName(Name);
            element.AddComponentDescription(Description);
            element.Add(new XAttribute(L5XName.Family, Family.Value));
            element.Add(new XAttribute(L5XName.Class, Class));


            var members = new XElement(nameof(L5XName.Members));
            members.Add(Members.Select(m => m.Serialize()));
            element.Add(members);

            return element;
        }

        /// <inheritdoc />
        public void Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != L5XName.DataType)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            Name = element.ComponentName();
            Description = element.ComponentDescription();
            Family = element.Attribute(L5XName.Family)?.Value.Parse<DataTypeFamily>() ?? DataTypeFamily.None;
            Members = element.Descendants(L5XName.Member)
                .Where(e => bool.Parse(e.Attribute(L5XName.Hidden)?.Value!) == false)
                .Select(e =>
                {
                    var member = new DataTypeMember();
                    member.Deserialize(e);
                    return member;
                }).ToList();
        }
    }
}