using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class LadderLogicSerializer : L5XSerializer<RllRoutine>
    {
        private static readonly XName ElementName = L5XName.RLLContent;

        public override XElement Serialize(RllRoutine component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(component.Select(r => RungSerializer.Serialize(r)));

            return element;
        }

        public override RllRoutine Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var rungs = element.Descendants(L5XName.Rung).Select(e => RungSerializer.Deserialize(e));

            return new RllRoutine(rungs);
        }
    }
}