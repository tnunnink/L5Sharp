using L5Sharp.Enums;

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="scope"></param>
        /// <param name="description"></param>
        protected Routine(string name, Scope? scope = null, string? description = null)
        {
            Name = name;
            Description = description ?? string.Empty;
            Scope = scope ?? Scope.Null;
        }
        
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public Scope Scope { get; set; }

        /// <summary>
        /// The type of the <see cref="Routine"/> component.
        /// </summary>
        /// <value>A <see cref="Enums.RoutineType"/> enum specifying the type content the routine contains.</value>
        public abstract RoutineType Type { get; }
    }
}