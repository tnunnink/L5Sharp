using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public class L5X : XElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public L5X(XElement content) : base(content)
        {
        }
        
        /// <summary>
        /// Gets the value of the schema revision for the current L5X content.
        /// </summary>
        /// <value>A <see cref="Revision"/> type that represent the major/minor revision of the L5X schema.</value>
        /// <remarks>This is always 1.0. If the R</remarks>
        public Revision? SchemaRevision => this.TryGetValue<Revision>(L5XName.SchemaRevision);

        /// <summary>
        /// Gets the value of the software revision for the current L5X content.
        /// </summary>
        /// <value>A <see cref="Revision"/> type that represent the major/minor revision of the software.</value>
        public Revision? SoftwareRevision => this.TryGetValue<Revision>(L5XName.SoftwareRevision);

        /// <summary>
        /// Gets the name of the Logix component that is the target of the current L5X context.
        /// </summary>
        public string? TargetName => this.TryGetValue<string>(L5XName.TargetName);

        /// <summary>
        /// Gets the type of Logix component that is the target of the current L5X context.
        /// </summary>
        public string? TargetType => this.TryGetValue<string>(L5XName.TargetType);

        /// <summary>
        /// Gets the value indicating whether the current L5X is contextual..
        /// </summary>
        public bool? ContainsContext => this.TryGetValue<bool>(L5XName.ContainsContext);

        /// <summary>
        /// Gets the owner that exported the current L5X file.
        /// </summary>
        public string? Owner => this.TryGetValue<string>(L5XName.Owner);

        /// <summary>
        /// Gets the date time that the L5X file was exported.
        /// </summary>
        public DateTime? ExportDate => this.LogixDateTimeOrDefault(L5XName.ExportDate);

        /*internal void NormalizeContent()
        {
            var container = new XElement(name);

            var controller = Element(L5XName.Controller) ?? GenerateContextController();

            controller.Add(container);
        }
        
        private */
    }
}