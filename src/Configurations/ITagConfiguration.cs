using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public interface ITagConfiguration : IComponentConfiguration<ITag>
    {
        ITagConfiguration HasDescription(string description);
        ITagConfiguration WithDimensions(Dimensions dimensions);
        ITagConfiguration WithRadix(Radix radix);
        ITagConfiguration WithAccess(ExternalAccess access);
        ITagConfiguration WithUsage(TagUsage usage);
        ITagConfiguration IsConstant();
    }
}