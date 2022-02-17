using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// An enumeration of options that specify the purpose of use of a L5X or <see cref="LogixContext"/>.
    /// </summary>
    public class Use : SmartEnum<Use, string>
    {
        private Use(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents a <see cref="Use"/> option that is invalid... 
        /// </summary>
        public static readonly Use Invalid = new(nameof(Invalid), nameof(Invalid));
        
        /// <summary>
        /// Get the Context <see cref="Use"/> option.
        /// </summary>
        public static readonly Use Context = new(nameof(Context), nameof(Context));
        
        /// <summary>
        /// Get the Create <see cref="Use"/> option.
        /// </summary>
        public static readonly Use Create = new(nameof(Create), nameof(Create));
        
        /// <summary>
        /// Get the Target <see cref="Use"/> option.
        /// </summary>
        public static readonly Use Target = new(nameof(Target), nameof(Target));
        
        /// <summary>
        /// Get the Update <see cref="Use"/> option.
        /// </summary>
        public static readonly Use Update = new(nameof(Update), nameof(Update));
        
        /// <summary>
        /// Get the Delete <see cref="Use"/> option.
        /// </summary>
        public static readonly Use Delete = new(nameof(Delete), nameof(Delete));
        
        /// <summary>
        /// Get the Insert <see cref="Use"/> option.
        /// </summary>
        public static readonly Use Insert = new(nameof(Insert), nameof(Insert));
        
        /// <summary>
        /// Get the Append <see cref="Use"/> option.
        /// </summary>
        public static readonly Use Append = new(nameof(Append), nameof(Append));
        
        /// <summary>
        /// Get the Redefine <see cref="Use"/> option.
        /// </summary>
        public static readonly Use Redefine = new(nameof(Redefine), nameof(Redefine));
        
        /// <summary>
        /// Get the Reference <see cref="Use"/> option.
        /// </summary>
        public static readonly Use Reference = new(nameof(Reference), nameof(Reference));
        
        /// <summary>
        /// Get the Overwrite <see cref="Use"/> option.
        /// </summary>
        public static readonly Use Overwrite = new(nameof(Overwrite), nameof(Overwrite));
    }
}