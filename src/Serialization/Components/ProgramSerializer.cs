using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class ProgramSerializer : IL5XSerializer<Program>
    {
        private static readonly XName ElementName = L5XElement.Program.ToXName();

        public XElement Serialize(Program component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, c => c.Name);
            element.AddElement(component, c => c.Description);
            element.AddAttribute(component, c => c.Type);
            element.AddAttribute(component, c => c.TestEdits);
            element.AddAttribute(component, c => c.MainRoutineName, p => !p.MainRoutineName.IsEmpty());
            element.AddAttribute(component, c => c.FaultRoutineName, p => !p.FaultRoutineName.IsEmpty());
            element.AddAttribute(component, c => c.Disabled);
            element.AddAttribute(component, c => c.UseAsFolder);

            var tags = new XElement(nameof(component.Tags));
            tags.Add(component.Tags.Select(t =>
            {
                var serializer = new TagSerializer();
                return serializer.Serialize(t);
            }));
            element.Add(tags);

            var routines = new XElement(nameof(component.Routines));
            routines.Add(component.Routines.Select(r =>
            {
                var serializer = new RoutineSerializer();
                return serializer.Serialize(r);
            }));
            element.Add(routines);

            return element;
        }

        public Program Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.GetComponentName();
            var description = element.GetComponentDescription();
            var testEdits = element.GetAttribute<IProgram, bool>(p => p.TestEdits);
            var mainRoutineName = element.GetAttribute<IProgram, string>(p => p.MainRoutineName);
            var faultRoutineName = element.GetAttribute<IProgram, string>(p => p.FaultRoutineName);
            var disabled = element.GetAttribute<IProgram, bool>(p => p.Disabled);
            var useAsFolder = element.GetAttribute<IProgram, bool>(p => p.UseAsFolder);

            var tags = element.Descendants(L5XElement.Tag.ToXName())
                .Select(e =>
                {
                    var serializer = new TagSerializer();
                    return serializer.Deserialize(e);
                });

            var routines = element.Descendants(L5XElement.Routine.ToXName())
                .Select(e =>
                {
                    var serializer = new RoutineSerializer();
                    return serializer.Deserialize(e);
                });

            return new Program(name, description, mainRoutineName, faultRoutineName, 
                useAsFolder, testEdits, disabled,
                tags, routines);
        }
    }
}