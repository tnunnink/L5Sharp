using System;
using System.Globalization;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class ControllerSerializer : L5XSerializer<IController>
    {
        private static readonly XName ElementName = L5XName.Controller;

        public override XElement Serialize(IController component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            if (component.ProcessorType is not null)
                element.Add(new XAttribute(L5XName.ProcessorType, component.ProcessorType));
            if (component.Revision is not null)
            {
                element.Add(new XAttribute(L5XName.MajorRev, component.Revision.Major));
                element.Add(new XAttribute(L5XName.MinorRev, component.Revision.Minor));
            }

            element.Add(new XAttribute(L5XName.ProjectCreationDate,
                component.ProjectCreationDate.ToString("ddd MMM d HH:mm:ss yyyy")));
            element.Add(new XAttribute(L5XName.LastModifiedDate,
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
            var processorType = element.Attribute(L5XName.ProcessorType)?.Value.Parse<CatalogNumber>();
            var major = element.Attribute(L5XName.MajorRev)?.Value;
            var minor = element.Attribute(L5XName.MinorRev)?.Value;
            var revision = Revision.Parse($"{major}.{minor}");
            var creationDate = DateTime.ParseExact(
                element.Attribute(L5XName.ProjectCreationDate)?.Value,
                "ddd MMM d HH:mm:ss yyyy",
                CultureInfo.CurrentCulture);
            var modifiedDate = DateTime.ParseExact(
                element.Attribute(L5XName.LastModifiedDate)?.Value,
                "ddd MMM d HH:mm:ss yyyy",
                CultureInfo.CurrentCulture);

            return new Controller(name, processorType, revision, creationDate, modifiedDate, description);
        }
    }
}