using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="Rung"/> components.
    /// </summary>
    public class RungSerializer : ILogixSerializer<Rung>
    {
        /// <inheritdoc />
        public XElement Serialize(Rung obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Rung);

            element.AddValue(obj, r => r.Number);
            element.AddValue(obj, r => r.Type);
            element.AddText(obj, r => r.Comment);
            element.AddText(obj, r => r.Text);
            
            return element;
        }

        /// <inheritdoc />
        public Rung Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new Rung
            {
                Number = element.GetValue<int>(L5XName.Number),
                Type = element.TryGetValue<RungType>(L5XName.Type) ?? RungType.Normal,
                Comment = element.TryGetValue<string>(L5XName.Comment) ?? string.Empty,
                Text = element.TryGetValue<NeutralText>(L5XName.Text) ?? NeutralText.Empty
            };
        }
    }
}