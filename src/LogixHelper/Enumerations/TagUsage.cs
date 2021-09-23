using Ardalis.SmartEnum;

namespace LogixHelper.Enumerations
{
    public class TagUsage : SmartEnum<TagUsage>
    {
        private TagUsage(string name, int value) : base(name, value)
        {
        }

        public static readonly TagUsage Normal = new TagUsage("Normal", 0);
        public static readonly TagUsage Local = new TagUsage("Local", 1);
        public static readonly TagUsage Input = new TagUsage("Input", 2);
        public static readonly TagUsage Output = new TagUsage("Output", 3);
        public static readonly TagUsage InOut = new TagUsage("InOut", 4);
        public static readonly TagUsage Static = new TagUsage("Static", 5);
        public static readonly TagUsage Null = new TagUsage("NULL", 6); //todo really?
    }
}