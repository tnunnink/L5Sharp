using System;
using System.Xml.Linq;
using L5Sharp.Core;

namespace L5Sharp.Components
{
    /// <inheritdoc />
    public class Controller : ILogixComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;


        public string? ProcessorType { get; set; } = string.Empty;


        public Revision? Revision { get; set; } = new();


        public DateTime ProjectCreationDate { get; set; } = DateTime.Now;


        public DateTime LastModifiedDate { get; set; } = DateTime.Now;

        /// <inheritdoc />
        public XElement Serialize()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Deserialize(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}