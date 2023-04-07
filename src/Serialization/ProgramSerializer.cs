using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="Program"/> components.
    /// </summary>
    public class ProgramSerializer : ILogixSerializer<Program>
    {
        private readonly TagSerializer _tagSerializer = new();
        private readonly RllSerializer _rllSerializer = new();
        private readonly StSerializer _stSerializer = new();
        
        /// <inheritdoc />
        public XElement Serialize(Program obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Program);
            
            element.AddValue(obj, p => p.Name);
            element.AddText(obj, p => p.Description);
            element.AddValue(obj, p => p.Type, t => t != ProgramType.Normal);
            element.AddValue(obj, p => p.TestEdits);
            element.AddValue(obj, p => p.MainRoutineName);
            element.AddValue(obj, p => p.FaultRoutineName);
            element.AddValue(obj, p => p.Disabled);
            element.AddValue(obj, p => p.UseAsFolder);

            var tags = new XElement(L5XName.Tags);
            tags.Add(obj.Tags.Select(t => _tagSerializer.Serialize(t)));
            element.Add(tags);

            var routines = new XElement(L5XName.Routines);
            
            element.Add(routines);

            return element;
        }

        /// <inheritdoc />
        public Program Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new Program
            {
                Name = element.LogixName(),
                Description = element.LogixDescription(),
                Type = element.TryGetValue<ProgramType>(L5XName.Type) ?? ProgramType.Normal,
                TestEdits = element.TryGetValue<bool>(L5XName.TestEdits),
                MainRoutineName = element.TryGetValue<string>(L5XName.MainRoutineName) ?? string.Empty,
                FaultRoutineName = element.TryGetValue<string>(L5XName.FaultRoutineName) ?? string.Empty,
                Disabled = element.TryGetValue<bool>(L5XName.Disabled),
                UseAsFolder = element.TryGetValue<bool>(L5XName.UseAsFolder),
                Tags = element.Descendants(L5XName.Tag).Select(t => _tagSerializer.Deserialize(t)).ToList()
            };
        }
    }
}