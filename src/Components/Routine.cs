using System;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Components
{
    /// <summary>
    /// Represents a Logix Routine component. <see cref="Routine"/> is an abstract base class for all routine types,
    /// including RLL, ST, FBD, and SFC.
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    public abstract class Routine : ILogixScopedComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <inheritdoc />
        public Scope Scope => Scope.Program;
        
        /// <summary>
        /// The type of the <see cref="Routine"/> component.
        /// </summary>
        /// <value>A <see cref="Enums.RoutineType"/> enum specifying the type content the routine contains.</value>
        public abstract RoutineType Type { get; }

        /// <inheritdoc />
        public virtual XElement Serialize()
        {
            var element = new XElement(L5XName.Routine);
            element.Add(new XAttribute(L5XName.Name, Name));
            element.Add(new XAttribute(L5XName.Type, Type.Value));

            return element;
        }

        /// <inheritdoc />
        public virtual void Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != L5XName.Routine)
                throw new ArgumentException($"Element '{element.Name}' not valid element for {GetType()}.");

            Name = element.ComponentName();
            Description = element.ComponentDescription();
        }
    }
}