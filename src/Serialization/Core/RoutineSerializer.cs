using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization.Core
{
    /// <summary>
    /// Provides serialization of a <see cref="IRoutine{TContent}"/> as represented in the L5X format. 
    /// </summary>
    public class RoutineSerializer : IXSerializer<IRoutine<ILogixContent>>
    {
        private static readonly XName ElementName = LogixNames.Routine;
        
        /// <inheritdoc />
        public XElement Serialize(IRoutine<ILogixContent> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.Type);
            element.Add(component.Content.Serialize());

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