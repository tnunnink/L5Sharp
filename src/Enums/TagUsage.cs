using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class TagUsage : SmartEnum<TagUsage, string>
    {
        private TagUsage(string name, string value) : base(name, value)
        {
        }

        public static readonly TagUsage Null = new TagUsage(nameof(Null), nameof(Null));
        public static readonly TagUsage Normal = new TagUsage(nameof(Normal), nameof(Normal));
        public static readonly TagUsage Local = new TagUsage(nameof(Local), nameof(Local));
        public static readonly TagUsage Input = new TagUsage(nameof(Input), nameof(Input));
        public static readonly TagUsage Output = new TagUsage(nameof(Output), nameof(Output));
        public static readonly TagUsage InOut = new TagUsage(nameof(InOut), nameof(InOut));
        public static readonly TagUsage Static = new TagUsage(nameof(Static), nameof(Static));
    }
}