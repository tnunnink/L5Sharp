﻿using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public class ControllerSerializer : ILogixSerializer<Controller>
    {
        /// <inheritdoc />
        public XElement Serialize(Controller obj)
        {
            Check.NotNull(obj);
            
            var element = new XElement(L5XName.Controller);
            
            element.AddValue(obj, c => c.Name);
            element.AddText(obj, c => c.Description);
            element.AddValue(obj, c => c.ProcessorType);

            if (obj.Revision is not null)
            {
                element.AddValue(obj.Revision.Major, L5XName.MajorRev);
                element.AddValue(obj.Revision.Minor, L5XName.MinorRev);
            }

            element.Add(new XAttribute(L5XName.ProjectCreationDate,
                obj.ProjectCreationDate.ToString("ddd MMM d HH:mm:ss yyyy")));
            element.Add(new XAttribute(L5XName.LastModifiedDate,
                obj.LastModifiedDate.ToString("ddd MMM d HH:mm:ss yyyy")));

            return element;
        }

        /// <inheritdoc />
        public Controller Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new Controller
            {
                Name = element.LogixName(),
                Description = element.LogixDescription(),
                ProcessorType = element.TryGetValue<string>(L5XName.ProcessorType) ?? string.Empty,
                Revision = GetRevision(element),
                ProjectCreationDate = element.LogixDateTimeOrDefault(L5XName.ProjectCreationDate),
                LastModifiedDate = element.LogixDateTimeOrDefault(L5XName.LastModifiedDate)
            };
        }

        private static Revision? GetRevision(XElement element)
        {
            var major = element.TryGetValue<string>(L5XName.MajorRev);
            var minor = element.TryGetValue<string>(L5XName.MinorRev);

            return major is not null && minor is not null ? Revision.Parse($"{major}.{minor}") : default;
        }
    }
}