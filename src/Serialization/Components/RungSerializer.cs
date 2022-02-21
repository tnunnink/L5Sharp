using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization.Components
{
    /// <summary>
    /// Provides serialization of a <see cref="Rung"/> as represented in the L5X format. 
    /// </summary>
    internal class RungSerializer : IL5XSerializer<Rung>
    {
        private static readonly XName ElementName = L5XElement.Rung.ToXName();

        /// <inheritdoc />
        public XElement Serialize(Rung component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddAttribute(component, c => c.Number);
            element.AddAttribute(component, c => c.Type.Value, nameOverride: nameof(component.Type));
            element.AddElement(component,c => c.Comment);
            element.AddElement(component, c => c.Text);
            
            return element;
        }
        
        /// <inheritdoc />
        public Rung Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var number = element.GetAttribute<Rung, int>(r => r.Number);
            var type = element.GetAttribute<Rung, RungType>(r => r.Type);
            var comment = element.GetElement<Rung, string>(r => r.Comment);
            var text = element.GetElement<Rung, NeutralText>(r => r.Text);

            return new Rung(number, type, comment, text);
        }
    }
}