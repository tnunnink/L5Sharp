using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// An enumeration of all <see cref="KeyingState"/> values for a given Logix <see cref="IModule"/>
    /// </summary>
    public class KeyingState : SmartEnum<KeyingState, string>
    {
        private KeyingState(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents the <c>ExactMatch</c> <see cref="KeyingState"/> value.
        /// </summary>
        public static readonly KeyingState ExactMatch = new(nameof(ExactMatch), nameof(ExactMatch));
        
        /// <summary>
        /// Represents the <c>CompatibleModule</c> <see cref="KeyingState"/> value.
        /// </summary>
        public static readonly KeyingState CompatibleModule = new(nameof(CompatibleModule), nameof(CompatibleModule));
        
        /// <summary>
        /// Represents the <c>Disabled</c> <see cref="KeyingState"/> value.
        /// </summary>
        public static readonly KeyingState Disabled = new(nameof(Disabled), nameof(Disabled));
        
        /// <summary>
        /// Represents the <c>Custom</c> <see cref="KeyingState"/> value.
        /// </summary>
        public static readonly KeyingState Custom = new(nameof(Custom), nameof(Custom));
    }
}