using System;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public sealed class Tag<TDataType> : ITag<TDataType> where TDataType : IDataType
    {
        private readonly ITagMember<TDataType> _tagMember;

        internal Tag(string name, TDataType dataType, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null, TagUsage? usage = null, bool constant = false, Comments? comments = null)
        {
            var root = (ITag<IDataType>)(ITag<TDataType>)this;
            var member = new Member<TDataType>(name, dataType, radix, externalAccess, description);
            _tagMember = new TagMember<TDataType>(member, root, null);

            Usage = usage != null ? usage : TagUsage.Null;
            Constant = constant;

            Comments = new Comments(comments);
            if (!string.IsNullOrEmpty(description))
                Comments.Apply(TagName, description);
        }

        /// <inheritdoc />
        public string Name => _tagMember.Name;

        /// <inheritdoc />
        public string Description => _tagMember.Description;

        /// <inheritdoc />
        public TagName TagName => _tagMember.TagName;

        /// <inheritdoc />
        public TDataType DataType => _tagMember.DataType;

        /// <inheritdoc />
        public Dimensions Dimensions => _tagMember.Dimensions;

        /// <inheritdoc />
        public Radix Radix => _tagMember.Radix;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess => _tagMember.ExternalAccess;

        /// <inheritdoc />
        public bool IsValueMember => _tagMember.IsValueMember;

        /// <inheritdoc />
        public bool IsStructureMember => _tagMember.IsStructureMember;

        /// <inheritdoc />
        public bool IsArrayMember => _tagMember.IsArrayMember;

        /// <inheritdoc />
        public object? Value => _tagMember.Value;

        /// <inheritdoc />
        public ITagMember<IDataType>? Parent => _tagMember.Parent;

        /// <inheritdoc />
        public ITag<IDataType> Root => _tagMember.Root;

        /// <inheritdoc />
        public TagType TagType => TagType.Base;

        /// <inheritdoc />
        public TagUsage Usage { get; }

        /// <inheritdoc />
        public bool Constant { get; }
        
        /// <inheritdoc />
        public Comments Comments { get; }

        /// <inheritdoc />
        public ITagMember<IDataType> this[int x] => _tagMember[x];

        /// <inheritdoc />
        public ITagMember<IDataType> this[int x, int y] => _tagMember[x, y];

        /// <inheritdoc />
        public ITagMember<IDataType> this[int x, int y, int z] => _tagMember[x, y, z];

        /// <inheritdoc />
        public void Comment(string comment) => _tagMember.Comment(comment);

        /// <inheritdoc />
        public ITagMember<IDataType>? FindMember(TagName tagName) => _tagMember.FindMember(tagName);

        /// <inheritdoc />
        public bool HasMember(TagName tagName) => _tagMember.HasMember(tagName);

        /// <inheritdoc />
        public ITagMember<IDataType> Member(TagName tagName) => _tagMember.Member(tagName);

        /// <inheritdoc />
        public ITagMember<TType> Member<TType>(Func<TDataType, IMember<TType>> selector)
            where TType : IDataType => _tagMember.Member(selector);

        /// <inheritdoc />
        public IEnumerable<ITagMember<IDataType>> Members() => _tagMember.Members();

        /// <inheritdoc />
        public IEnumerable<TagName> TagNames() => _tagMember.TagNames();

        /// <inheritdoc />
        public bool TrySetValue(IAtomicType? value) => _tagMember.TrySetValue(value);

        /// <inheritdoc />
        public void SetValue(IAtomicType value) => _tagMember.SetValue(value);

        /// <inheritdoc />
        public void SetData(IComplexType dataType) => _tagMember.SetData(dataType);
    }
}