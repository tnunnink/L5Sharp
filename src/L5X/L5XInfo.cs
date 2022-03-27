using System;
using System.Globalization;
using L5Sharp.Core;

namespace L5Sharp.L5X
{
    /// <summary>
    /// A set of meta data properties about a given L5X export file.
    /// </summary>
    public class L5XInfo
    {
        private const string DateFormat = "ddd MMM d HH:mm:ss yyyy";
        private readonly L5XDocument _document;

        /// <summary>
        /// Creates a new <see cref="L5XInfo"/> object with the provided <see cref="L5XDocument"/>.
        /// </summary>
        internal L5XInfo(L5XDocument document)
        {
            _document = document;
        }

        /// <summary>
        /// Gets the value of the schema revision for the current L5X context.
        /// </summary>
        public Revision SchemaRevision =>
            Revision.Parse(_document.Content.Attribute(L5XAttribute.SchemaRevision.ToString())?.Value!);

        /// <summary>
        /// Gets the value of the software revision for the current L5X context.
        /// </summary>
        public Revision SoftwareRevision =>
            Revision.Parse(_document.Content.Attribute(L5XAttribute.SoftwareRevision.ToString())?.Value!);

        /// <summary>
        /// Gets the name of the Logix component that is the target of the current L5X context.
        /// </summary>
        public ComponentName TargetName =>
            new(_document.Content.Attribute(L5XAttribute.TargetName.ToString())?.Value!);

        /// <summary>
        /// Gets the type of Logix component that is the target of the current L5X context.
        /// </summary>
        public string TargetType => _document.Content.Attribute(L5XAttribute.TargetType.ToString())?.Value!;

        /// <summary>
        /// Gets the value indicating whether the current L5X is contextual..
        /// </summary>
        public bool ContainsContext =>
            bool.Parse(_document.Content.Attribute(L5XAttribute.ContainsContext.ToString())?.Value!);

        /// <summary>
        /// Gets the owner that exported the current L5X file.
        /// </summary>
        public string Owner => _document.Content.Attribute(L5XAttribute.Owner.ToString())?.Value!;

        /// <summary>
        /// Gets the date time that the L5X file was exported.
        /// </summary>
        public DateTime ExportDate => 
            DateTime.ParseExact(_document.Content.Attribute(L5XAttribute.ExportDate.ToString())?.Value, DateFormat,
            CultureInfo.CurrentCulture);
    }
}