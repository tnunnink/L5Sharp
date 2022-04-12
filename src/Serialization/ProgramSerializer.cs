using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class ProgramSerializer : L5XSerializer<IProgram>
    {
        private readonly L5XContent? _document;
        private static readonly XName ElementName = L5XElement.Program.ToString();
        
        private TagSerializer TagSerializer => _document is not null
            ? _document.Serializers.Get<TagSerializer>()
            : new TagSerializer(_document);
        
        private RoutineSerializer RoutineSerializer => _document is not null
            ? _document.Serializers.Get<RoutineSerializer>()
            : new RoutineSerializer(_document);
        

        public ProgramSerializer(L5XContent? document = null)
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
            element.Add(new XAttribute(L5XAttribute.Type.ToString(), component.Type));
            element.Add(new XAttribute(L5XAttribute.TestEdits.ToString(), component.TestEdits));
            if (!component.MainRoutineName.IsEmpty())
                element.Add(new XAttribute(L5XAttribute.MainRoutineName.ToString(), component.MainRoutineName));
            if (!component.FaultRoutineName.IsEmpty())
                element.Add(new XAttribute(L5XAttribute.FaultRoutineName.ToString(), component.FaultRoutineName));
            element.Add(new XAttribute(L5XAttribute.Disabled.ToString(), component.Disabled));
            element.Add(new XAttribute(L5XAttribute.UseAsFolder.ToString(), component.UseAsFolder));

            var tags = new XElement(L5XElement.Tags.ToString());
            tags.Add(component.Tags.Select(t => TagSerializer.Serialize(t)));
            element.Add(tags);

            var routines = new XElement(L5XElement.Routines.ToString());
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
            var testEdits = element.Attribute(L5XAttribute.TestEdits.ToString())?.Value.Parse<bool>() ?? default;
            var mainRoutineName = element.Attribute(L5XAttribute.MainRoutineName.ToString())?.Value;
            var faultRoutineName = element.Attribute(L5XAttribute.FaultRoutineName.ToString())?.Value;
            var disabled = element.Attribute(L5XAttribute.Disabled.ToString())?.Value.Parse<bool>() ?? default;
            var useAsFolder = element.Attribute(L5XAttribute.UseAsFolder.ToString())?.Value.Parse<bool>() ?? default;

            var tags = element.Descendants(L5XElement.Tag.ToString())
                .Select(e => TagSerializer.Deserialize(e));

            var routines = element.Descendants(L5XElement.Routine.ToString())
                .Where(e => e.Attribute(L5XAttribute.Type.ToString())?.Value == RoutineType.Rll.Value)
                .Select(e => RoutineSerializer.Deserialize(e));

            return new Program(name, description, mainRoutineName, faultRoutineName,
                useAsFolder, testEdits, disabled, tags, routines);
        }
    }
}