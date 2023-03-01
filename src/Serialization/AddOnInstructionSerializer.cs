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
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="AddOnInstruction"/> components.
    /// </summary>
    public class AddOnInstructionSerializer : ILogixSerializer<AddOnInstruction>
    {
        private const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.fff'Z'";
        private readonly ParameterSerializer _parameterSerializer = new();

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
            element.AddValue(obj, o => o.CreatedDate);
            element.AddValue(obj, o => o.CreatedBy);
            element.AddValue(obj, o => o.EditedDate);
            element.AddValue(obj, o => o.EditedBy);
            element.AddValue(obj, o => o.SoftwareRevision);
            element.AddText(obj, o => o.AdditionalHelpText);
            element.AddValue(obj, o => o.IsEncrypted);

            /*element.Add(new XAttribute(L5XName.CreatedDate,
                component.CreatedDate.ToString(DateTimeFormat)));*/

            /*element.Add(new XAttribute(L5XName.EditedDate,
                component.EditedDate.ToString(DateTimeFormat)));*/

            /*element.Add(new XAttribute(L5XName.SoftwareRevision, $"v{component.SoftwareRevision}"));*/
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
                SoftwareRevision = element.TryGetValue<Revision>(L5XName.SoftwareRevision) ?? new Revision(),
                AdditionalHelpText = element.TryGetValue<string>(L5XName.AdditionalHelpText) ?? string.Empty,
                IsEncrypted = element.TryGetValue<bool>(L5XName.IsEncrypted),
                Parameters = element.Descendants(L5XName.Parameter).Select(e => _parameterSerializer.Deserialize(e))
                    .ToList(),
                
            };
        }
    }
}