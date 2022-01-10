using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Represents an enumeration of Logix <see cref="TagType"/> options for a given <see cref="ITag{TDataType}"/>.
    /// </summary>
    public abstract class TagType : SmartEnum<TagType, string>
    {
        private TagType(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents a Base <see cref="TagType"/> value.
        /// </summary>
        public static readonly TagType Base = new BaseType();
        
        /// <summary>
        /// Represents a Alias <see cref="TagType"/> value.
        /// </summary>
        public static readonly TagType Alias = new AliasType();
        
        /// <summary>
        /// Represents a Produced <see cref="TagType"/> value.
        /// </summary>
        public static readonly TagType Produced = new ProducedType();
        
        /// <summary>
        /// Represents a Consumed <see cref="TagType"/> value.
        /// </summary>
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