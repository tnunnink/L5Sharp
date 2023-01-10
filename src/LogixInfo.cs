using System;
using System.Globalization;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Utilities;

namespace L5Sharp
{
    /// <summary>
    /// A wrapper class around an <see cref="XDocument"/> that provided generic methods for getting various
    /// component elements based on component type. Also performs validation on the provided L5X XDocument.
    /// </summary>
    public class LogixInfo
    {
        private const string DefaultRevision = "1.0";
        private const string DateFormat = "ddd MMM d HH:mm:ss yyyy";
        private readonly XElement _root;

        internal LogixInfo(XElement root)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
        }

        public static LogixInfo Create(ILogixComponent target)
        {
            var content = new XElement(L5XName.RSLogix5000Content);

            content.Add(new XAttribute(L5XName.SchemaRevision, DefaultRevision));
            content.Add(new XAttribute(L5XName.SoftwareRevision, DefaultRevision));
            content.Add(new XAttribute(L5XName.TargetName, target.Name));
            content.Add(new XAttribute(L5XName.TargetType, target.GetType()));
            content.Add(new XAttribute(L5XName.ContainsContext, target.GetType() != typeof(Controller)));
            content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
            content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateFormat)));

            return new LogixInfo(content);
        }

        public static LogixInfo Empty()
        {
            var content = new XElement(L5XName.RSLogix5000Content);

            content.Add(new XAttribute(L5XName.SchemaRevision, DefaultRevision));
            content.Add(new XAttribute(L5XName.SoftwareRevision, DefaultRevision));
            content.Add(new XAttribute(L5XName.ContainsContext, true));
            content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
            content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateFormat)));

            return new LogixInfo(content);
        }

        /// <summary>
        /// Gets the value of the schema revision for the current L5X context.
        /// </summary>
        public Revision SchemaRevision =>
            Revision.Parse(_root.Attribute(L5XName.SchemaRevision)?.Value!);

        /// <summary>
        /// Gets the value of the software revision for the current L5X context.
        /// </summary>
        public Revision SoftwareRevision =>
            Revision.Parse(_root.Attribute(L5XName.SoftwareRevision)?.Value!);

        /// <summary>
        /// Gets the name of the Logix component that is the target of the current L5X context.
        /// </summary>
        public ComponentName? TargetName
        {
            get
            {
                var name = _root.Attribute(L5XName.TargetName)?.Value;
                return name is not null ? new ComponentName(name) : null;
            }
        }

        /// <summary>
        /// Gets the type of Logix component that is the target of the current L5X context.
        /// </summary>
        public string? TargetType => _root.Attribute(L5XName.TargetType)?.Value;

        /// <summary>
        /// Gets the value indicating whether the current L5X is contextual..
        /// </summary>
        public bool ContainsContext =>
            bool.Parse(_root.Attribute(L5XName.ContainsContext)?.Value!);

        /// <summary>
        /// Gets the owner that exported the current L5X file.
        /// </summary>
        public string? Owner => _root.Attribute(L5XName.Owner)?.Value;

        /// <summary>
        /// Gets the date time that the L5X file was exported.
        /// </summary>
        public DateTime ExportDate =>
            DateTime.ParseExact(_root.Attribute(L5XName.ExportDate)?.Value, DateFormat,
                CultureInfo.CurrentCulture);
    }
}