using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class RoutineSerializer : L5XSerializer<IRoutine<ILogixContent>>
    {
        private static readonly XName ElementName = L5XElement.Routine.ToString();
        private readonly LadderLogicSerializer _ladderLogicSerializer;

        public RoutineSerializer()
        {
            _ladderLogicSerializer = new LadderLogicSerializer();
        }

        public override XElement Serialize(IRoutine<ILogixContent> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XAttribute.Type.ToString(), component.Type));

            if (component.Content is ILadderLogic ladderLogic)
            {
                element.Add(_ladderLogicSerializer.Serialize(ladderLogic));
            }

            return element;
        }

        public override IRoutine<ILogixContent> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var type = element.Attribute(L5XAttribute.Type.ToString())?.Value.Parse<RoutineType>()!;

            var content = type.CreateContent();

            type
                .When(RoutineType.Rll).Then(() =>
                {
                    var rll = element.Elements().FirstOrDefault();
                    if (rll is null) return;
                    content = _ladderLogicSerializer.Deserialize(rll);
                });

            return new Routine<ILogixContent>(name, type, description, content);
        }
    }
}