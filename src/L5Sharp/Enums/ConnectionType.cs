using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class ConnectionType : SmartEnum<ConnectionType>
    {
        private ConnectionType(string name, int value) : base(name, value)
        {
        }

        public static readonly ConnectionType Unknown = new ConnectionType("Unknown", 0);
        public static readonly ConnectionType Input = new ConnectionType("Input", 1);
        public static readonly ConnectionType Output = new ConnectionType("Output", 2);
        public static readonly ConnectionType MotionSync = new ConnectionType("MotionSync", 3);
        public static readonly ConnectionType MotionAsync = new ConnectionType("MotionAsync", 4);
        public static readonly ConnectionType MotionEvent = new ConnectionType("MotionEvent", 5);
        public static readonly ConnectionType SafetyInput = new ConnectionType("SafetyInput", 6);
        public static readonly ConnectionType SafetyOutput = new ConnectionType("SafetyOutput", 7);
    }
}