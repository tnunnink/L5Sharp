using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public class TagConfiguration : ITagConfiguration
    {
        private readonly Tag _tag;

        public TagConfiguration(string name, IDataType dataType)
        {
            _tag = new Tag(name, dataType);
        }
        
        public ITag Compile()
        {
            return _tag;
        }

        public ITagConfiguration HasDescription(string description)
        {
            _tag.SetDescription(description);
            return this;
        }

        public ITagConfiguration WithDimensions(Dimensions dimensions)
        {
            _tag.ChangeDimensions(dimensions);
            return this;
        }

        public ITagConfiguration WithRadix(Radix radix)
        {
            _tag.SetRadix(radix);
            return this;
        }

        public ITagConfiguration WithAccess(ExternalAccess access)
        {
            _tag.SetExternalAccess(access);
            return this;
        }

        public ITagConfiguration WithUsage(TagUsage usage)
        {
            _tag.SetUsage(usage);
            return this;
        }

        public ITagConfiguration IsConstant()
        {
            _tag.Constant = false;
            return this;
        }
    }
}