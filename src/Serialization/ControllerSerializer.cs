using System;
using System.Globalization;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization
{
    internal class ControllerSerializer : IXSerializer<IController>
    {
        private const string ElementName = LogixNames.Controller;

        public XElement Serialize(IController component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.Add(component.ToAttribute(c => c.Name));
            element.Add(component.ToAttribute(c => c.ProcessorType));
            element.Add(component.ToAttribute(c => c.Revision.Major));
            element.Add(component.ToAttribute(c => c.Revision.Minor));
            
            element.Add(new XAttribute(nameof(component.ProjectCreationDate),
                component.ProjectCreationDate.ToString("ddd MMM d HH:mm:ss yyyy")));
            
            element.Add(new XAttribute(nameof(component.LastModifiedDate),
                component.ProjectCreationDate.ToString("ddd MMM d HH:mm:ss yyyy")));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(component.ToElement(c => c.Description));

            return element;
        }

        public IController Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException(
                    $"Element name '{element.Name}' invalid. Expecting '{ElementName}'.");

            var name = element.GetName();
            var processorType = element.GetValue<IController, ProcessorType>(c => c.ProcessorType);
            var description = element.GetValue<IController, string>(c => c.Description);

            var major = element.GetValue<IController, ushort>(c => c.Revision.Major);
            var minor = element.GetValue<IController, ushort>(c => c.Revision.Minor);
            var revision = new Revision(major, minor);

            var creationDate = DateTime.ParseExact(element.Attribute("ProjectCreationDate")?.Value,
                "ddd MMM d HH:mm:ss yyyy", CultureInfo.CurrentCulture);

            var modifiedDate = DateTime.ParseExact(element.Attribute("LastModifiedDate")?.Value,
                "ddd MMM d HH:mm:ss yyyy", CultureInfo.CurrentCulture);

            return new Controller(name!, processorType!, revision, creationDate, modifiedDate, description);
        }
    }
}