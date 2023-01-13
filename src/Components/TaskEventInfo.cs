using L5Sharp.Enums;

namespace L5Sharp.Components
{
    /// <summary>
    /// Represents a Logix Event Task component.
    /// </summary>
    public sealed class TaskEventInfo
    {
        /// <summary>
        /// Gets the <see cref="Enums.TaskEventTrigger"/> setting of the <see cref="TaskEventInfo"/> instance.
        /// </summary>
        public TaskEventTrigger EventTrigger { get; set; } = TaskEventTrigger.EventInstructionOnly;

        /// <summary>
        /// Gets the value indicating whether the <see cref="TaskEventInfo"/> timeout setting is enabled.
        /// </summary>
        public bool EnableTimeout { get; set; } = false;

        /// <summary>
        /// Gets the tag name that triggers the <see cref="TaskEventInfo"/>.
        /// </summary>
        public string? EventTag { get; set; } = string.Empty;
    }
}