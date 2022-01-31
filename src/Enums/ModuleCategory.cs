using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Represents an enumeration of <see cref="ModuleCategory"/> for a given <see cref="IModule"/>.
    /// </summary>
    public sealed class ModuleCategory : SmartEnum<ModuleCategory>
    {
        private ModuleCategory(string name, int value) : base(name, value)
        {
        }
        
        

        /// <summary>
        /// Represents an Analog <see cref="ModuleCategory"/>.
        /// </summary>
        public static readonly ModuleCategory Analog = new(nameof(Analog), 0);
        
        /// <summary>
        /// Represents a Digital <see cref="ModuleCategory"/>.
        /// </summary>
        public static readonly ModuleCategory Digital = new(nameof(Digital), 1);
        
        /// <summary>
        /// Represents a Communication <see cref="ModuleCategory"/>.
        /// </summary>
        public static readonly ModuleCategory Communication = new(nameof(Communication), 2);
        
        /// <summary>
        /// Represents a Controller <see cref="ModuleCategory"/>.
        /// </summary>
        public static readonly ModuleCategory Controller = new(nameof(Controller), 3);
        
        /// <summary>
        /// Represents a Motion <see cref="ModuleCategory"/>.
        /// </summary>
        public static readonly ModuleCategory Motion = new(nameof(Motion), 4);
        
        /// <summary>
        /// Represents a Specialty <see cref="ModuleCategory"/>.
        /// </summary>
        public static readonly ModuleCategory Specialty = new(nameof(Specialty), 5);
    }
}