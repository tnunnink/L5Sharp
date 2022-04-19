using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class AddOnInstructionSerializer : L5XSerializer<IAddOnInstruction>
    {
        private readonly L5XContent? _document;
        private const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.fff'Z'";
        private static readonly XName ElementName = L5XName.AddOnInstructionDefinition;

        private ParameterSerializer ParameterSerializer => _document is not null
            ? _document.Serializers.Get<ParameterSerializer>()
            : new ParameterSerializer(_document);
        
        private LocalTagSerializer LocalTagSerializer => _document is not null
            ? _document.Serializers.Get<LocalTagSerializer>()
            : new LocalTagSerializer(_document);
        
        private RoutineSerializer RoutineSerializer => _document is not null
            ? _document.Serializers.Get<RoutineSerializer>()
            : new RoutineSerializer(_document);

        public AddOnInstructionSerializer(L5XContent? document = null)
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
            element.Add(new XAttribute(L5XName.Revision, component.Revision));
            if (!component.RevisionExtension.IsEmpty())
                element.Add(new XAttribute(L5XName.RevisionExtension, component.RevisionExtension));
            if (!component.RevisionNote.IsEmpty())
                element.Add(new XElement(L5XName.RevisionNote, new XCData(component.RevisionNote)));
            if (!component.Vendor.IsEmpty())
                element.Add(new XAttribute(L5XName.Vendor, component.Vendor));
            element.Add(new XAttribute(L5XName.ExecutePrescan, component.ExecutePreScan));
            element.Add(new XAttribute(L5XName.ExecutePostscan, component.ExecutePostScan));
            element.Add(new XAttribute(L5XName.ExecuteEnableInFalse, component.ExecuteEnableInFalse));
            element.Add(new XAttribute(L5XName.CreatedDate,
                component.CreatedDate.ToString(DateTimeFormat)));
            element.Add(new XAttribute(L5XName.CreatedBy, component.CreatedBy));
            element.Add(new XAttribute(L5XName.EditedDate,
                component.EditedDate.ToString(DateTimeFormat)));
            element.Add(new XAttribute(L5XName.EditedBy, component.EditedBy));
            element.Add(new XAttribute(L5XName.SoftwareRevision, $"v{component.SoftwareRevision}"));
            if (!component.AdditionalHelpText.IsEmpty())
                element.Add(new XElement(L5XName.AdditionalHelpText,
                    new XCData(component.AdditionalHelpText)));
            element.Add(new XAttribute(L5XName.IsEncrypted, component.IsEncrypted));

            var parameters = new XElement(L5XName.Parameters);
            parameters.Add(component.Parameters.Select(p => ParameterSerializer.Serialize(p)));
            element.Add(parameters);

            var tags = new XElement(L5XName.LocalTags);
            tags.Add(component.LocalTags.Select(t => LocalTagSerializer.Serialize(t)));
            element.Add(tags);

            var routines = new XElement(L5XName.Routines);
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
            var revision = element.Attribute(L5XName.Revision)?.Value.Parse<Revision>();
            var revisionExtension = element.Attribute(L5XName.RevisionExtension)?.Value;
            var revisionNote = element.Element(L5XName.RevisionNote)?.Value;
            var vendor = element.Attribute(L5XName.Vendor)?.Value;
            var executePrescan = element.Attribute(L5XName.ExecutePrescan)
                ?.Value.Parse<bool>() ?? default;
            var executePostscan = element.Attribute(L5XName.ExecutePostscan)
                ?.Value.Parse<bool>() ?? default;
            var executeEnableInFalse = element.Attribute(L5XName.ExecuteEnableInFalse)
                ?.Value.Parse<bool>() ?? default;
            var createdDate = DateTime.ParseExact(element.Attribute(L5XName.CreatedDate)?.Value,
                DateTimeFormat, CultureInfo.CurrentCulture);
            var createdBy = element.Attribute(L5XName.CreatedBy)?.Value;
            var editedDate = DateTime.ParseExact(element.Attribute(L5XName.EditedDate)?.Value,
                DateTimeFormat, CultureInfo.CurrentCulture);
            var editedBy = element.Attribute(L5XName.EditedBy)?.Value;
            var softwareRevision = element.Attribute(L5XName.SoftwareRevision)?.Value
                .TrimStart('v').Parse<Revision>();
            var additionalHelpText = element.Element(L5XName.AdditionalHelpText)?.Value;
            var isEncrypted = element.Attribute(L5XName.IsEncrypted)?.Value.Parse<bool>() ?? default;

            var parameters = element.Descendants(L5XName.Parameter)
                .Select(e => ParameterSerializer.Deserialize(e));

            var tags = element.Descendants(L5XName.LocalTag)
                .Select(e => LocalTagSerializer.Deserialize(e));

            var routines = element.Descendants(L5XName.Routine)
                .Select(e => RoutineSerializer.Deserialize(e));

            return new AddOnInstruction(name, description, revision, revisionExtension, revisionNote, vendor,
                executePrescan, executePostscan, executeEnableInFalse, createdDate, createdBy, editedDate, editedBy,
                softwareRevision, additionalHelpText, isEncrypted, parameters, tags, routines);
        }
    }
}