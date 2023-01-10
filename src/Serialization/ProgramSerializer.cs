using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class ProgramSerializer : L5XSerializer<IProgram>
    {
        private readonly LogixInfo? _document;
        private static readonly XName ElementName = L5XName.Program;
        
        private TagSerializer TagSerializer => _document is not null
            ? _document.Serializers.Get<TagSerializer>()
            : new TagSerializer(_document);
        
        private RoutineSerializer RoutineSerializer => _document is not null
            ? _document.Serializers.Get<RoutineSerializer>()
            : new RoutineSerializer(_document);
        

        public ProgramSerializer(LogixInfo? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IProgram component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XName.Type, component.Type));
            element.Add(new XAttribute(L5XName.TestEdits, component.TestEdits));
            if (!component.MainRoutineName.IsEmpty())
                element.Add(new XAttribute(L5XName.MainRoutineName, component.MainRoutineName));
            if (!component.FaultRoutineName.IsEmpty())
                element.Add(new XAttribute(L5XName.FaultRoutineName, component.FaultRoutineName));
            element.Add(new XAttribute(L5XName.Disabled, component.Disabled));
            element.Add(new XAttribute(L5XName.UseAsFolder, component.UseAsFolder));

            var tags = new XElement(L5XName.Tags);
            tags.Add(component.Tags.Select(t => TagSerializer.Serialize(t)));
            element.Add(tags);

            var routines = new XElement(L5XName.Routines);
            routines.Add(component.Routines.Select(r => RoutineSerializer.Serialize(r)));
            element.Add(routines);

            return element;
        }

        public override IProgram Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var testEdits = element.Attribute(L5XName.TestEdits)?.Value.Parse<bool>() ?? default;
            var mainRoutineName = element.Attribute(L5XName.MainRoutineName)?.Value;
            var faultRoutineName = element.Attribute(L5XName.FaultRoutineName)?.Value;
            var disabled = element.Attribute(L5XName.Disabled)?.Value.Parse<bool>() ?? default;
            var useAsFolder = element.Attribute(L5XName.UseAsFolder)?.Value.Parse<bool>() ?? default;

            var tags = element.Descendants(L5XName.Tag)
                .Select(e => TagSerializer.Deserialize(e));

            var routines = element.Descendants(L5XName.Routine)
                .Where(e => e.Attribute(L5XName.Type)?.Value == RoutineType.Rll.Value)
                .Select(e => RoutineSerializer.Deserialize(e));

            return new Program(name, description, mainRoutineName, faultRoutineName,
                useAsFolder, testEdits, disabled, tags, routines);
        }
    }
}