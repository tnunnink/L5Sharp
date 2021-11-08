using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public class TagConfiguration : ITagConfiguration
    {
        private readonly string _name;
        private string _description;
        private IDataType _dataType;
        private Dimensions _dimensions;
        private Radix _radix;
        private ExternalAccess _externalAccess;
        private TagUsage _usage;
        private bool _constant;

        private TagConfiguration(string name)
        {
            _name = name;
        }
        
        public ITag<IDataType> Compile()
        {
            return new Tag<IDataType>(_name, _dataType, _dimensions, _radix, _externalAccess, _description, _usage,
                _constant);
        }

        public static ITagConfiguration New(string name)
        {
            var configuration = new TagConfiguration(name);
            return configuration;
        }
        
        public ITagConfiguration OfType(IDataType dataType)
        {
            _dataType = dataType;
            return this;
        }

        public ITagConfiguration HasDescription(string description)
        {
            _description = description;
            return this;
        }

        public ITagConfiguration WithDimensions(Dimensions dimensions)
        {
            _dimensions = dimensions;
            return this;
        }
        
        public ITagConfiguration WithRadix(Radix radix)
        {
            _radix = radix;
            return this;
        }

        public ITagConfiguration WithAccess(ExternalAccess access)
        {
            _externalAccess = access;
            return this;
        }

        public ITagConfiguration WithUsage(TagUsage usage)
        {
            _usage = usage;
            return this;
        }

        public ITagConfiguration IsConstant()
        {
            _constant = true;
            return this;
        }
    }
}