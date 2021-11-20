using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Represents an enumeration of all <c>TaskTypes</c> (i.e. Continuous, Periodic, Event)
    /// </summary>
    public sealed class TaskType : SmartEnum<TaskType, string>
    {
        private TaskType(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// A Continuous TaskType.
        /// </summary>
        public static readonly TaskType Continuous = 
            new TaskType(nameof(Continuous).ToUpper(), nameof(Continuous).ToUpper());
        
        /// <summary>
        /// A Periodic TaskType.
        /// </summary>
        public static readonly TaskType Periodic = 
            new TaskType(nameof(Periodic).ToUpper(), nameof(Periodic).ToUpper());
        
        /// <summary>
        /// A Event TaskType.
        /// </summary>
        public static readonly TaskType Event = 
            new TaskType(nameof(Event).ToUpper(), nameof(Event).ToUpper());
    }
}