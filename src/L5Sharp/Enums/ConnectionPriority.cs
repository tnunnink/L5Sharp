using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class ConnectionPriority : SmartEnum<ConnectionPriority>
    {
        public ConnectionPriority(string name, int value) : base(name, value)
        {
        }

        public static readonly ConnectionPriority Low = new ConnectionPriority("Low", 0);
        public static readonly ConnectionPriority High = new ConnectionPriority("High", 1);
        public static readonly ConnectionPriority Scheduled = new ConnectionPriority("Scheduled", 2);
        public static readonly ConnectionPriority Urgent = new ConnectionPriority("Urgent", 3);
    }
}