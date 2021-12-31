using System;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// Provides serialization of a <see cref="Rung"/> as represented in the L5X format. 
    /// </summary>
    internal class RungSerializer : IXSerializer<Rung>
    {
        private static readonly XName ElementName = LogixNames.Rung;

        /// <inheritdoc />
        public XElement Serialize(Rung component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddValue(component, c => c.Number);
            element.AddValue(component, c => c.Type);
            element.AddValue(component,c => c.Comment, true);
            element.AddValue(component, c => c.Text, true);
            
            return element;
        }
        
        /// <inheritdoc />
        public Rung Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var number = element.GetValue<Rung, int>(r => r.Number);
            var type = element.GetValue<Rung, RungType>(r => r.Type);
            var comment = element.GetValue<Rung, string>(r => r.Comment);
            var text = element.GetValue<Rung, NeutralText>(r => r.Text);

            return new Rung(number, type, comment, text);
        }
    }
}