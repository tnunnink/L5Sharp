using System;
using System.Globalization;
using System.Xml.Linq;
using L5Sharp.Core;

namespace L5Sharp.L5X
{
    /// <summary>
    /// A set of meta data properties about a given L5X export file.
    /// </summary>
    public class L5XInfo
    {
        private readonly XElement _content;
        private const string DefaultRevision = "1.0";
        private const string DateFormat = "ddd MMM d HH:mm:ss yyyy";

        /// <summary>
        /// Creates a new <see cref="L5XInfo"/> object with the provided <see cref="L5XDocument"/>.
        /// </summary>
        internal L5XInfo(XElement content)
        {
            _content = content;
        }

        internal static XElement Default()
        {
            var element = new XElement(L5XElement.RSLogix5000Content.ToString());
            
            element.Add(new XAttribute(L5XAttribute.SchemaRevision.ToString(), DefaultRevision));
            element.Add(new XAttribute(L5XAttribute.SoftwareRevision.ToString(), DefaultRevision));
            element.Add(new XAttribute(L5XAttribute.ContainsContext.ToString(), false));
            element.Add(new XAttribute(L5XAttribute.Owner.ToString(), Environment.UserName));
            element.Add(new XAttribute(L5XAttribute.ExportDate.ToString(), DateTime.Now.ToString(DateFormat)));

            return element;
        } 
        
        internal static XElement New(string targetName, string targetType)
        {
            var element = new XElement(L5XElement.RSLogix5000Content.ToString());
            
            element.Add(new XAttribute(L5XAttribute.SchemaRevision.ToString(), DefaultRevision));
            element.Add(new XAttribute(L5XAttribute.SoftwareRevision.ToString(), DefaultRevision));
            element.Add(new XAttribute(L5XAttribute.TargetName.ToString(), targetName));
            element.Add(new XAttribute(L5XAttribute.TargetType.ToString(), targetType));
            element.Add(new XAttribute(L5XAttribute.ContainsContext.ToString(), false));
            element.Add(new XAttribute(L5XAttribute.Owner.ToString(), Environment.UserName));
            element.Add(new XAttribute(L5XAttribute.ExportDate.ToString(), DateTime.Now.ToString(DateFormat)));

            return element;
        }

        /// <summary>
        /// Gets the value of the schema revision for the current L5X context.
        /// </summary>
        public Revision SchemaRevision =>
            Revision.Parse(_content.Attribute(L5XAttribute.SchemaRevision.ToString())?.Value!);

        /// <summary>
        /// Gets the value of the software revision for the current L5X context.
        /// </summary>
        public Revision SoftwareRevision =>
            Revision.Parse(_content.Attribute(L5XAttribute.SoftwareRevision.ToString())?.Value!);

        /// <summary>
        /// Gets the name of the Logix component that is the target of the current L5X context.
        /// </summary>
        public ComponentName? TargetName
        {
            get
            {
                var name = _content.Attribute(L5XAttribute.TargetName.ToString())?.Value;
                return name is not null ? new ComponentName(name) : null;
            }
        }

        /// <summary>
        /// Gets the type of Logix component that is the target of the current L5X context.
        /// </summary>
        public string? TargetType => _content.Attribute(L5XAttribute.TargetType.ToString())?.Value;

        /// <summary>
        /// Gets the value indicating whether the current L5X is contextual..
        /// </summary>
        public bool ContainsContext =>
            bool.Parse(_content.Attribute(L5XAttribute.ContainsContext.ToString())?.Value!);

        /// <summary>
        /// Gets the owner that exported the current L5X file.
        /// </summary>
        public string? Owner => _content.Attribute(L5XAttribute.Owner.ToString())?.Value;

        /// <summary>
        /// Gets the date time that the L5X file was exported.
        /// </summary>
        public DateTime ExportDate => 
            DateTime.ParseExact(_content.Attribute(L5XAttribute.ExportDate.ToString())?.Value, DateFormat,
            CultureInfo.CurrentCulture);
    }
}