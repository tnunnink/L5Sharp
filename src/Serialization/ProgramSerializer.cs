using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    internal class ProgramSerializer : IXSerializer<Program>
    {
        private readonly LogixContext _context;
        private static readonly XName ElementName = LogixNames.Program;

        public ProgramSerializer(LogixContext context)
        {
            _context = context;
        }

        public XElement Serialize(Program component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            
            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.Type);
            element.AddAttribute(component, c => c.TestEdits);
            element.AddAttribute(component, c => c.Disabled);

            if (component.Tags.Count > 0)
            {
                var tags = new XElement(nameof(component.Tags));
                tags.Add(component.Tags.Select(t => _context.Serializer.Serialize(t)));
                element.Add(tags);
            }
            
            if (component.Routines.Count > 0)
            {
                var routines = new XElement(nameof(component.Routines));
                routines.Add(component.Routines.Select(t => _context.Serializer.Serialize(t)));
                element.Add(routines);
            }

            return element;
        }

        public Program Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var type = element.GetAttribute<IProgram, ProgramType>(p => p.Type);
            
            var name = element.GetComponentName();
            var description = element.GetComponentDescription();
            var testEdits = element.GetAttribute<IProgram, bool>(p => p.TestEdits);
            var disabled = element.GetAttribute<IProgram, bool>(p => p.Disabled);

            var tags = element.Descendants(LogixNames.Tag)
                .Select(e => _context.Serializer.Deserialize<ITag<IDataType>>(e));
            
            var routines = element.Descendants(LogixNames.Routine)
                .Select(e => _context.Serializer.Deserialize<IRoutine<ILogixContent>>(e));

            return new Program(name, description, testEdits: testEdits, disabled: disabled,
                tags: tags, routines: routines);
        }
    }
}