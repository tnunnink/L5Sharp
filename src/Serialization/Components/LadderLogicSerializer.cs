using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization.Components
{
    internal class LadderLogicSerializer : IL5XSerializer<ILadderLogic>
    {
        private static readonly XName ElementName = L5XElement.RLLContent.ToXName();
        
        public XElement Serialize(ILadderLogic component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.Add(component.Select(r =>
            {
                var serializer = new RungSerializer();
                return serializer.Serialize(r);
            }));

            return element;
        }
        
        public ILadderLogic Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var rungs = element.Descendants(L5XElement.Rung.ToXName())
                .Select(e =>
                {
                    var serializer = new RungSerializer();
                    return serializer.Deserialize(e);
                });

            return new LadderLogic(rungs);
        }
    }
}