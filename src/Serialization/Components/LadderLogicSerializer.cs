using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class LadderLogicSerializer : L5XSerializer<ILadderLogic>
    {
        private static readonly XName ElementName = L5XElement.RLLContent.ToString();
        private readonly RungSerializer _rungSerializer;

        public LadderLogicSerializer()
        {
            _rungSerializer = new RungSerializer();
        }

        public override XElement Serialize(ILadderLogic component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(component.Select(r => _rungSerializer.Serialize(r)));

            return element;
        }

        public override ILadderLogic Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var rungs = element.Descendants(L5XElement.Rung.ToString()).Select(e => _rungSerializer.Deserialize(e));

            return new LadderLogic(rungs);
        }
    }
}