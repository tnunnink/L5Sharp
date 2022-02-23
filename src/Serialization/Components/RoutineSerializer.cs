using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization.Components
{
    internal class RoutineSerializer : IL5XSerializer<IRoutine<ILogixContent>>
    {
        private static readonly XName ElementName = L5XElement.Routine.ToXName();
        
        public XElement Serialize(IRoutine<ILogixContent> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.Type);

            if (component.Content is ILadderLogic ladderLogic)
            {
                var serializer = new LadderLogicSerializer();
                element.Add(serializer.Serialize(ladderLogic));
            }

            return element;
        }
        
        public IRoutine<ILogixContent> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetComponentName();
            var description = element.GetComponentDescription();
            var type = element.GetAttribute<IRoutine<ILogixContent>, RoutineType>(r => r.Type)!;

            var content = type.CreateContent();

            type
                .When(RoutineType.Rll).Then(() =>
                {
                    var rll = element.Elements().FirstOrDefault();
                    if (rll is null) return;
                    var serializer = new LadderLogicSerializer();
                    content = serializer.Deserialize(rll);

                });

            return new Routine<ILogixContent>(name, type, description, content);
        }
    }
}