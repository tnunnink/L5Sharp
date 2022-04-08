using System;
using System.Globalization;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class ControllerSerializer : L5XSerializer<IController>
    {
        private static readonly XName ElementName = L5XElement.Controller.ToString();

        public override XElement Serialize(IController component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XAttribute.ProcessorType.ToString(), component.ProcessorType));
            element.Add(new XAttribute(L5XAttribute.MajorRev.ToString(), component.Revision.Major));
            element.Add(new XAttribute(L5XAttribute.MinorRev.ToString(), component.Revision.Minor));
            element.Add(new XAttribute(L5XAttribute.ProjectCreationDate.ToString(),
                component.ProjectCreationDate.ToString("ddd MMM d HH:mm:ss yyyy")));
            element.Add(new XAttribute(L5XAttribute.LastModifiedDate.ToString(),
                component.ProjectCreationDate.ToString("ddd MMM d HH:mm:ss yyyy")));

            return element;
        }

        public override IController Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var processorType = element.Attribute(L5XAttribute.ProcessorType.ToString())?.Value.Parse<CatalogNumber>();
            var major = element.Attribute(L5XAttribute.MajorRev.ToString())?.Value;
            var minor = element.Attribute(L5XAttribute.MinorRev.ToString())?.Value;
            var revision = Revision.Parse($"{major}.{minor}");
            var creationDate = DateTime.ParseExact(
                element.Attribute(L5XAttribute.ProjectCreationDate.ToString())?.Value,
                "ddd MMM d HH:mm:ss yyyy",
                CultureInfo.CurrentCulture);
            var modifiedDate = DateTime.ParseExact(
                element.Attribute(L5XAttribute.LastModifiedDate.ToString())?.Value,
                "ddd MMM d HH:mm:ss yyyy",
                CultureInfo.CurrentCulture);

            return new Controller(name, processorType, revision, creationDate, modifiedDate, description);
        }
    }
}