using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public abstract class TagType : SmartEnum<TagType, string>
    {
        private TagType(string name, string value) : base(name, value)
        {
        }

        public static readonly TagType Base = new BaseType();
        public static readonly TagType Alias = new AliasType();
        public static readonly TagType Produced = new ProducedType();
        public static readonly TagType Consumed = new ConsumedType();

        private class BaseType : TagType
        {
            public BaseType() : base(nameof(Base), nameof(Base))
            {
            }
        }

        private class AliasType : TagType
        {
            public AliasType() : base(nameof(Alias), nameof(Alias))
            {
            }
        }

        private class ProducedType : TagType
        {
            public ProducedType() : base(nameof(Produced), nameof(Produced))
            {
            }
        }

        private class ConsumedType : TagType
        {
            public ConsumedType() : base(nameof(Consumed), nameof(Consumed))
            {
            }
        }
    }
}