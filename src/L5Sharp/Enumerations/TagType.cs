using Ardalis.SmartEnum;

namespace L5Sharp.Enumerations
{
    public class TagType : SmartEnum<TagType>
    {
        private TagType(string name, int value) : base(name, value)
        {
        }

        public static readonly TagType Base = new TagType("Base", 0);
        public static readonly TagType Alias = new TagType("Alias", 1);
        public static readonly TagType Produced = new TagType("Produced", 2);
        public static readonly TagType Consumed = new TagType("Consumed", 3);
    }
}