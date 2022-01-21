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
            string? description = null, TagUsage? usage = null, bool constant = false)
        {
            externalAccess ??= ExternalAccess.None; // tags are different than members in the default.
            var member = new Member<TDataType>(name, dataType, radix, externalAccess, description);
            _tagMember = new TagMember<TDataType>(member, Root, Parent);
            
            Usage = usage != null ? usage : TagUsage.Null;
            Scope = Scope.Null;
            Constant = constant;
            Comments = new Comments(Root);

            //We need to store the default description if it is provided.
            if (!string.IsNullOrEmpty(description))
                Comments.Set(new Comment(TagName, description));
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
        public ITagMember<IDataType>? Parent => null;

        /// <inheritdoc />
        public ITag<IDataType> Root => (ITag<IDataType>)(ITag<TDataType>)this;

        /// <inheritdoc />
        public TagType TagType => TagType.Base;

        /// <inheritdoc />
        public Scope Scope { get; }

        /// <inheritdoc />
        public TagUsage Usage { get; }

        /// <inheritdoc />
        public bool Constant { get; }

        /// <inheritdoc />
        public Comments Comments { get; }

        /// <inheritdoc />
        public void Comment(string comment) => _tagMember.Comment(comment);

        /// <inheritdoc />
        public bool Contains(TagName tagName) => _tagMember.Contains(tagName);

        /// <inheritdoc />
        public void SetValue(IAtomicType value) => _tagMember.SetValue(value);

        /// <inheritdoc />
        public void SetData(IComplexType dataType) => _tagMember.SetData(dataType);

        /// <inheritdoc />
        public bool TrySetValue(IAtomicType? value) => _tagMember.TrySetValue(value);

        /// <inheritdoc />
        public ITagMember<IDataType> this[int x] => _tagMember[x];

        /// <inheritdoc />
        public ITagMember<IDataType> this[int x, int y] => _tagMember[x, y];

        /// <inheritdoc />
        public ITagMember<IDataType> this[int x, int y, int z] => _tagMember[x, y, z];

        /// <inheritdoc />
        public ITagMember<IDataType> this[TagName tagName] => _tagMember[tagName];

        /// <inheritdoc />
        public ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> selector)
            where TType : IDataType => _tagMember.GetMember(selector);

        /// <inheritdoc />
        public IEnumerable<ITagMember<IDataType>> GetMembers() => _tagMember.GetMembers();

        /// <inheritdoc />
        public bool TryGetMember(TagName tagName, out ITagMember<IDataType>? tagMember) =>
            _tagMember.TryGetMember(tagName, out tagMember);

        /// <inheritdoc />
        public IEnumerable<TagName> GetTagNames() => _tagMember.GetTagNames();
    }
}