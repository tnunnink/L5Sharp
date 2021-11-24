using System;
using System.Globalization;
using System.Xml.Linq;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// Represents the <c>RSLogix5000Content</c> information is the header data for all L5X files.
    /// </summary>
    public class L5XInfo
    {
        /// <summary>
        /// Creates a new instance of the <c>LogixContent</c> object. 
        /// </summary>
        internal L5XInfo(XElement element)
        {
            SchemaRevision = Revision.Parse(element.Attribute(nameof(SchemaRevision))?.Value);
            SoftwareRevision = Revision.Parse(element.Attribute(nameof(SoftwareRevision))?.Value);
            TargetName = new ComponentName(element.Attribute(nameof(TargetName))?.Value);
            TargetType = element.Attribute(nameof(TargetType))?.Value;
            ContainsContext = bool.Parse(element.Attribute(nameof(ContainsContext))?.Value!);
            Owner = element.Attribute(nameof(Owner))?.Value;
            ExportDate = DateTime.ParseExact(element.Attribute(nameof(ExportDate))?.Value, "ddd MMM d HH:mm:ss yyyy",
                CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// The revision of the L5X Schema.
        /// </summary>
        public Revision SchemaRevision { get; }

        /// <summary>
        /// The revision of the Software (i.e. the controller revision).
        /// </summary>
        public Revision SoftwareRevision { get; }

        /// <summary>
        /// The name of the target Logix component for the current context.
        /// </summary>
        public ComponentName TargetName { get; }

        /// <summary>
        /// The type of the target Logix component for the current context.
        /// </summary>
        public string TargetType { get; }

        /// <summary>
        /// Indicates whether the current context represents an entire controller or components of a project.
        /// </summary>
        public bool ContainsContext { get; }

        /// <summary>
        /// The user name and company of the current context.
        /// </summary>
        public string Owner { get; }

        /// <summary>
        /// The date the current context was exported.
        /// </summary>
        public DateTime ExportDate { get; }
    }
}