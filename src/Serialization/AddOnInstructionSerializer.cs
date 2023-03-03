using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Serialization.Data;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="AddOnInstruction"/> components.
    /// </summary>
    public class AddOnInstructionSerializer : ILogixSerializer<AddOnInstruction>
    {
        private const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.fff'Z'";
        private readonly ParameterSerializer _parameterSerializer = new();
        private readonly FormattedDataSerializer _dataSerializer = new();
        private readonly RllRoutineSerializer _rllSerializer = new();

        /// <inheritdoc />
        public XElement Serialize(AddOnInstruction obj)
        {
            Check.NotNull(obj);

            var element = new XElement(typeof(AddOnInstruction).GetLogixName());

            element.AddValue(obj, o => o.Name);
            element.AddText(obj, o => o.Description);
            element.AddValue(obj, o => o.Revision);
            element.AddText(obj, o => o.RevisionExtension);
            element.AddText(obj, o => o.RevisionNote);
            element.AddText(obj, o => o.Vendor);
            element.AddValue(obj, o => o.ExecutePreScan);
            element.AddValue(obj, o => o.ExecutePostScan);
            element.AddValue(obj, o => o.ExecuteEnableInFalse);
            element.Add(new XAttribute(L5XName.CreatedDate,
                obj.CreatedDate.ToString(DateTimeFormat)));
            element.AddValue(obj, o => o.CreatedBy);
            element.Add(new XAttribute(L5XName.EditedDate,
                obj.EditedDate.ToString(DateTimeFormat)));
            element.AddValue(obj, o => o.EditedBy);
            element.AddValue(obj, o => o.SoftwareRevision);
            element.AddText(obj, o => o.AdditionalHelpText);
            element.AddValue(obj, o => o.IsEncrypted);

            var parameters = new XElement(L5XName.Parameters);
            parameters.Add(obj.Parameters.Select(p => _parameterSerializer.Serialize(p)));
            element.Add(parameters);
            
            var localTags = new XElement(L5XName.LocalTags);
            localTags.Add(obj.LocalTags.Select(SerializeLocalTag));
            element.Add(localTags);

            var routines = new XElement(L5XName.Routines);
            foreach (var routine in obj.Routines)
            {
                var routineSerializer = LogixSerializer.GetSerializer<Routine>(routine.GetType());
                routines.Add(routineSerializer.Serialize(routine));
            }
            element.Add(routines);

            return element;
        }

        /// <inheritdoc />
        public AddOnInstruction Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new AddOnInstruction
            {
                Name = element.LogixName(),
                Description = element.LogixDescription(),
                Revision = element.TryGetValue<Revision>(L5XName.Revision) ?? new Revision(),
                RevisionExtension = element.TryGetValue<string>(L5XName.RevisionExtension) ?? string.Empty,
                RevisionNote = element.TryGetValue<string>(L5XName.RevisionNote) ?? string.Empty,
                Vendor = element.TryGetValue<string>(L5XName.Vendor) ?? string.Empty,
                ExecutePreScan = element.TryGetValue<bool>(L5XName.ExecutePrescan),
                ExecutePostScan = element.TryGetValue<bool>(L5XName.ExecutePostscan),
                ExecuteEnableInFalse = element.TryGetValue<bool>(L5XName.ExecuteEnableInFalse),
                CreatedDate = element.TryGetValue<DateTime>(L5XName.CreatedDate),
                CreatedBy = element.TryGetValue<string>(L5XName.CreatedBy) ?? string.Empty,
                EditedDate = element.TryGetValue<DateTime>(L5XName.EditedDate),
                EditedBy = element.TryGetValue<string>(L5XName.EditedBy) ?? string.Empty,
                SoftwareRevision = Revision.Parse(element.Attribute(L5XName.SoftwareRevision)?.Value.Trim('v')!),
                AdditionalHelpText = element.TryGetValue<string>(L5XName.AdditionalHelpText) ?? string.Empty,
                IsEncrypted = element.TryGetValue<bool>(L5XName.IsEncrypted),
                Parameters = element.Descendants(L5XName.Parameter).Select(e => _parameterSerializer.Deserialize(e))
                    .ToList(),
                LocalTags = element.Descendants(L5XName.LocalTag).Select(DeserializeLocalTag).ToList(),
                Routines = DeserializeRoutines(element).ToList()
            };
        }

        private IEnumerable<Routine> DeserializeRoutines(XContainer element)
        {
            var routines = element.Descendants(L5XName.Routine).ToList();

            if (routines.All(r => r.GetValue<RoutineType>(L5XName.Type) == RoutineType.Rll))
                return routines.Select(r => (Routine)_rllSerializer.Deserialize(r));

            //todo would support other types here but for now not doing that.

            return Enumerable.Empty<Routine>();
        }

        private Tag DeserializeLocalTag(XElement element)
        {
            var data = element.Descendants(L5XName.DefaultData)
                .FirstOrDefault(e => e.Attribute(L5XName.Format)?.Value != DataFormat.L5K);

            return new Tag
            {
                Name = element.LogixName(),
                Description = element.LogixDescription(),
                Data = data is not null ? _dataSerializer.Deserialize(data) : Logix.Null,
                ExternalAccess = element.TryGetValue<ExternalAccess>(L5XName.ExternalAccess) ?? ExternalAccess.None,
                TagType = TagType.Base,
                Usage = TagUsage.Local,
                AliasFor = TagName.Empty
            };
        }
        
        private XElement SerializeLocalTag(Tag tag)
        {
            var element = new XElement(L5XName.LocalTag);
            
            element.AddValue(tag, t => t.Name);
            element.AddText(tag, t => t.Description);
            element.AddValue(tag, t => t.DataType);
            element.AddValue(tag, t => t.Radix, r => r != Radix.Null);
            element.AddValue(tag, t => t.ExternalAccess);
            element.Add(_dataSerializer.Serialize(tag.Data));

            return element;
        }
    }
}