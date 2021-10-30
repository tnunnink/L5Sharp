using System.Runtime.CompilerServices;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

[assembly: InternalsVisibleTo("L5Sharp.Core.Tests")]

namespace L5Sharp.Core
{
    public class Tag : TagBase
    {
        internal Tag(string name, IDataType dataType, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false, ILogixComponent parent = null) : base(name, dataType, dimensions, radix, externalAccess,
            description, usage, constant, parent)
        {
        }

        public override TagType TagType => TagType.Base;
    }

    /*public class Tag<TDataType> : TagBase<TDataType> where TDataType : IDataType, new()
    {
        internal Tag(string name, ILogixComponent parent = null, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false)
            : base(name, new TDataType(), dimensions, radix, externalAccess, description, parent, usage, constant)
        {
        }

        internal Tag(Tag tag)
            : base(tag.Name, (TDataType)tag.DataType, tag.Dimensions, tag.Radix, tag.ExternalAccess, tag.Description,
                tag.Parent, tag.Usage, tag.Constant)
        {
        }

        internal Tag(ITag<IDataType> tag) : base(tag.Name, (TDataType)tag.DataType, tag.Dimensions, tag.Radix,
            tag.ExternalAccess, tag.Description, tag.Parent, tag.Usage, tag.Constant)
        {
        }

        public override TagType TagType => TagType.Base;
    }*/
}