using System;
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

        /// <inheritdoc />
        public XElement Serialize(IRoutine<ILogixContent> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.Type);
            //todo logix content

            return element;
        }

        /// <inheritdoc />
        public IRoutine<ILogixContent> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetComponentName();
            var description = element.GetComponentDescription();
            var type = element.GetAttribute<IRoutine<ILogixContent>, RoutineType>(r => r.Type);
            
            //todo content ??
            /*var content = element.Element(LogixNames.RllContent) is not null 
                ? element.Element(LogixNames.RllContent).Deserialize<IRllContent>() : null;*/

            return new Routine<ILogixContent>(name, type, description);
        }
    }
}