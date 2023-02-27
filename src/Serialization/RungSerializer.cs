using System;
using System.Xml.Linq;
using L5Sharp.Common;
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
                Number = element.Value<int>(L5XName.Number),
                Type = element.Value<RungType>(L5XName.Type),
                Comment = element.ValueOrDefault<string>(L5XName.Comment) ?? string.Empty,
                Text = element.Value<NeutralText>(L5XName.Text)
            };
        }
    }
}