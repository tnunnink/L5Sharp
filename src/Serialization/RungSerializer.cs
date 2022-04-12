using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// Provides serialization of a <see cref="Rung"/> as represented in the L5X format. 
    /// </summary>
    internal class RungSerializer : L5XSerializer<Rung>
    {
        private static readonly XName ElementName = L5XElement.Rung.ToString();

        /// <inheritdoc />
        public override XElement Serialize(Rung component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(new XAttribute(L5XAttribute.Number.ToString(), component.Number));
            element.Add(new XAttribute(L5XAttribute.Type.ToString(), component.Type.Value));
            if (!component.Comment.IsEmpty())
                element.Add(new XElement(L5XElement.Comment.ToString(), new XCData(component.Comment)));
            element.Add(new XElement(L5XElement.Text.ToString(), new XCData(component.Text)));

            return element;
        }

        /// <inheritdoc />
        public override Rung Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var number = element.Attribute(L5XAttribute.Number.ToString())?.Value.Parse<int>() ?? default;
            var type = element.Attribute(L5XAttribute.Type.ToString())?.Value.Parse<RungType>();
            var comment = element.Element(L5XElement.Comment.ToString())?.Value;
            var text = element.Element(L5XElement.Text.ToString())?.Value.Parse<NeutralText>();

            return new Rung(number, type, comment, text);
        }
    }
}