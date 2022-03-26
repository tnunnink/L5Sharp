using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class AddOnInstructionSerializer : L5XSerializer<IAddOnInstruction>
    {
        private readonly L5XDocument? _document;
        private const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.fff'Z'";
        private static readonly XName ElementName = L5XElement.AddOnInstructionDefinition.ToString();

        private ParameterSerializer ParameterSerializer => _document is not null
            ? _document.Serializers.Get<ParameterSerializer>()
            : new ParameterSerializer(_document);
        
        private LocalTagSerializer LocalTagSerializer => _document is not null
            ? _document.Serializers.Get<LocalTagSerializer>()
            : new LocalTagSerializer(_document);
        
        private RoutineSerializer RoutineSerializer => _document is not null
            ? _document.Serializers.Get<RoutineSerializer>()
            : new RoutineSerializer(_document);

        public AddOnInstructionSerializer(L5XDocument? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IAddOnInstruction component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XAttribute.Revision.ToString(), component.Revision));
            if (!component.RevisionExtension.IsEmpty())
                element.Add(new XAttribute(L5XAttribute.RevisionExtension.ToString(), component.RevisionExtension));
            if (!component.RevisionNote.IsEmpty())
                element.Add(new XElement(L5XElement.RevisionNote.ToString(), new XCData(component.RevisionNote)));
            if (!component.Vendor.IsEmpty())
                element.Add(new XAttribute(L5XAttribute.Vendor.ToString(), component.Vendor));
            element.Add(new XAttribute(L5XAttribute.ExecutePrescan.ToString(), component.ExecutePreScan));
            element.Add(new XAttribute(L5XAttribute.ExecutePostscan.ToString(), component.ExecutePostScan));
            element.Add(new XAttribute(L5XAttribute.ExecuteEnableInFalse.ToString(), component.ExecuteEnableInFalse));
            element.Add(new XAttribute(L5XAttribute.CreatedDate.ToString(),
                component.CreatedDate.ToString(DateTimeFormat)));
            element.Add(new XAttribute(L5XAttribute.CreatedBy.ToString(), component.CreatedBy));
            element.Add(new XAttribute(L5XAttribute.EditedDate.ToString(),
                component.EditedDate.ToString(DateTimeFormat)));
            element.Add(new XAttribute(L5XAttribute.EditedBy.ToString(), component.EditedBy));
            element.Add(new XAttribute(L5XAttribute.SoftwareRevision.ToString(), $"v{component.SoftwareRevision}"));
            if (!component.AdditionalHelpText.IsEmpty())
                element.Add(new XElement(L5XElement.AdditionalHelpText.ToString(),
                    new XCData(component.AdditionalHelpText)));
            element.Add(new XAttribute(L5XAttribute.IsEncrypted.ToString(), component.IsEncrypted));

            var parameters = new XElement(L5XElement.Parameters.ToString());
            parameters.Add(component.Parameters.Select(p => ParameterSerializer.Serialize(p)));
            element.Add(parameters);

            var tags = new XElement(L5XElement.LocalTags.ToString());
            tags.Add(component.LocalTags.Select(t => LocalTagSerializer.Serialize(t)));
            element.Add(tags);

            var routines = new XElement(L5XElement.Routines.ToString());
            routines.Add(component.Routines.Select(r => RoutineSerializer.Serialize(r)));
            element.Add(routines);

            return element;
        }

        public override IAddOnInstruction Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var revision = element.Attribute(L5XAttribute.Revision.ToString())?.Value.Parse<Revision>();
            var revisionExtension = element.Attribute(L5XAttribute.RevisionExtension.ToString())?.Value;
            var revisionNote = element.Element(L5XElement.RevisionNote.ToString())?.Value;
            var vendor = element.Attribute(L5XAttribute.Vendor.ToString())?.Value;
            var executePrescan = element.Attribute(L5XAttribute.ExecutePrescan.ToString())
                ?.Value.Parse<bool>() ?? default;
            var executePostscan = element.Attribute(L5XAttribute.ExecutePostscan.ToString())
                ?.Value.Parse<bool>() ?? default;
            var executeEnableInFalse = element.Attribute(L5XAttribute.ExecuteEnableInFalse.ToString())
                ?.Value.Parse<bool>() ?? default;
            var createdDate = DateTime.ParseExact(element.Attribute(L5XAttribute.CreatedDate.ToString())?.Value,
                DateTimeFormat, CultureInfo.CurrentCulture);
            var createdBy = element.Attribute(L5XAttribute.CreatedBy.ToString())?.Value;
            var editedDate = DateTime.ParseExact(element.Attribute(L5XAttribute.EditedDate.ToString())?.Value,
                DateTimeFormat, CultureInfo.CurrentCulture);
            var editedBy = element.Attribute(L5XAttribute.EditedBy.ToString())?.Value;
            var softwareRevision = element.Attribute(L5XAttribute.SoftwareRevision.ToString())?.Value
                .TrimStart('v').Parse<Revision>();
            var additionalHelpText = element.Element(L5XElement.AdditionalHelpText.ToString())?.Value;
            var isEncrypted = element.Attribute(L5XAttribute.IsEncrypted.ToString())?.Value.Parse<bool>() ?? default;

            var parameters = element.Descendants(L5XElement.Parameter.ToString())
                .Select(e => ParameterSerializer.Deserialize(e));

            var tags = element.Descendants(L5XElement.LocalTag.ToString())
                .Select(e => LocalTagSerializer.Deserialize(e));

            var routines = element.Descendants(L5XElement.Routine.ToString())
                .Select(e => RoutineSerializer.Deserialize(e));

            return new AddOnInstruction(name, description, revision, revisionExtension, revisionNote, vendor,
                executePrescan, executePostscan, executeEnableInFalse, createdDate, createdBy, editedDate, editedBy,
                softwareRevision, additionalHelpText, isEncrypted, parameters, tags, routines);
        }
    }
}