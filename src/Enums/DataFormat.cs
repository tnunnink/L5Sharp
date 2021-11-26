using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class DataFormat : SmartEnum<DataFormat>
    {
        private DataFormat(string name, int value) : base(name, value)
        {
        }

        public static readonly DataFormat Decorated = new DataFormat("Decorated", 0);
        public static readonly DataFormat L5K = new DataFormat("L5K", 1);
        public static readonly DataFormat String = new DataFormat("String", 2);
        public static readonly DataFormat Alarm = new DataFormat("Alarm", 3);
    }
}