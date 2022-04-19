using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using L5Sharp.Core;

namespace L5Sharp.L5X
{
    /// <summary>
    /// A wrapper class around an <see cref="XDocument"/> that provided generic methods for getting various
    /// component elements based on component type. Also performs validation on the provided L5X XDocument.
    /// </summary>
    internal class L5XContent
    {
        private const string DefaultRevision = "1.0";
        private const string DateFormat = "ddd MMM d HH:mm:ss yyyy";
        private const string L5XSchema = "L5Sharp.Resources.L5X.xsd";

        
        internal L5XContent(XElement content)
        {
            //todo need to decide how to get valid schema file.
            //We should probably create our own using exports and xsd generation tools...
            //ValidateL5X(document);
            
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Index = new L5XIndex(this);
            Serializers = new L5XSerializers(this);
        }

       
        public static L5XContent Create(ILogixComponent target)
        {
            var content = new XElement(L5XName.RSLogix5000Content);
            
            content.Add(new XAttribute(L5XName.SchemaRevision, DefaultRevision));
            content.Add(new XAttribute(L5XName.SoftwareRevision, DefaultRevision));
            content.Add(new XAttribute(L5XName.TargetName, target.Name));
            content.Add(new XAttribute(L5XName.TargetType, target.GetType()));
            content.Add(new XAttribute(L5XName.ContainsContext, target.GetType() != typeof(IController)));
            content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
            content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateFormat)));

            return new L5XContent(content);
        }
        
        public static L5XContent Empty()
        {
            var content = new XElement(L5XName.RSLogix5000Content);
            
            content.Add(new XAttribute(L5XName.SchemaRevision, DefaultRevision));
            content.Add(new XAttribute(L5XName.SoftwareRevision, DefaultRevision));
            content.Add(new XAttribute(L5XName.ContainsContext, true));
            content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
            content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateFormat)));

            return new L5XContent(content);
        }
        
        /// <summary>
        /// Gets the value of the schema revision for the current L5X context.
        /// </summary>
        public Revision SchemaRevision =>
            Revision.Parse(Content.Attribute(L5XName.SchemaRevision)?.Value!);

        /// <summary>
        /// Gets the value of the software revision for the current L5X context.
        /// </summary>
        public Revision SoftwareRevision =>
            Revision.Parse(Content.Attribute(L5XName.SoftwareRevision)?.Value!);

        /// <summary>
        /// Gets the name of the Logix component that is the target of the current L5X context.
        /// </summary>
        public ComponentName? TargetName
        {
            get
            {
                var name = Content.Attribute(L5XName.TargetName)?.Value;
                return name is not null ? new ComponentName(name) : null;
            }
        }

        /// <summary>
        /// Gets the type of Logix component that is the target of the current L5X context.
        /// </summary>
        public string? TargetType => Content.Attribute(L5XName.TargetType)?.Value;

        /// <summary>
        /// Gets the value indicating whether the current L5X is contextual..
        /// </summary>
        public bool ContainsContext =>
            bool.Parse(Content.Attribute(L5XName.ContainsContext)?.Value!);

        /// <summary>
        /// Gets the owner that exported the current L5X file.
        /// </summary>
        public string? Owner => Content.Attribute(L5XName.Owner)?.Value;

        /// <summary>
        /// Gets the date time that the L5X file was exported.
        /// </summary>
        public DateTime ExportDate => 
            DateTime.ParseExact(Content.Attribute(L5XName.ExportDate)?.Value, DateFormat,
                CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets the root content <see cref="XElement"/> of the L5X file.
        /// </summary>
        public XElement Content { get; }

        /// <summary>
        /// Gets the <see cref="L5XIndex"/> for the current <see cref="L5XContent"/>.
        /// </summary>
        public L5XIndex Index { get; }

        /// <summary>
        /// Gets the <see cref="L5XSerializers"/> for the current <see cref="L5XContent"/>.
        /// </summary>
        public L5XSerializers Serializers { get; }
        
        public XElement? Controller => Content.Element(L5XName.Controller);
        public IEnumerable<XElement> DataTypes => Content.Descendants(L5XName.DataType);
        public IEnumerable<XElement> Modules => Content.Descendants(L5XName.Module);
        public IEnumerable<XElement> Instructions => Content.Descendants(L5XName.AddOnInstructionDefinition);
        public IEnumerable<XElement> Tags => Content.Descendants(L5XName.Tag);
        public IEnumerable<XElement> Programs => Content.Descendants(L5XName.Program);
        public IEnumerable<XElement> Tasks => Content.Descendants(L5XName.Task);

        private void ValidateL5X(XDocument document)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(L5XSchema);

            if (stream is null)
                throw new InvalidOperationException(
                    $"Could not load embedded resource '{L5XSchema} from current assembly.");

            var schema = XmlSchema.Read(stream, null);
            var schemaSet = new XmlSchemaSet();
            schemaSet.Add(schema);

            document.Validate(schemaSet, ValidationEventHandler);
        }

        private void ValidationEventHandler(object sender, ValidationEventArgs e) =>
            throw new XmlException();
    }
}