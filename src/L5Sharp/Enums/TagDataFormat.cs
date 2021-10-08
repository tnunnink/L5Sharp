using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class TagDataFormat : SmartEnum<TagDataFormat>
    {
        private TagDataFormat(string name, int value) : base(name, value)
        {
        }

        public static readonly TagDataFormat Decorated = new TagDataFormat("Decorated", 0);
        public static readonly TagDataFormat L5K = new TagDataFormat("L5K", 1);
        public static readonly TagDataFormat String = new TagDataFormat("String", 2);
        public static readonly TagDataFormat Alarm = new TagDataFormat("Alarm", 3);
    }
}