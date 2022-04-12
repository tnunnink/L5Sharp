using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class LadderLogicSerializer : L5XSerializer<ILadderLogic>
    {
        private readonly L5XContent? _document;
        private static readonly XName ElementName = L5XElement.RLLContent.ToString();

        private RungSerializer RungSerializer => _document is not null
            ? _document.Serializers.Get<RungSerializer>()
            : new RungSerializer();

        public LadderLogicSerializer(L5XContent? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(ILadderLogic component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.Add(component.Select(r => RungSerializer.Serialize(r)));

            return element;
        }

        public override ILadderLogic Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var rungs = element.Descendants(L5XElement.Rung.ToString()).Select(e => RungSerializer.Deserialize(e));

            return new LadderLogic(rungs);
        }
    }
}