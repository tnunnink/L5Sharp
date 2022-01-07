using System;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public sealed class Tag<TDataType> : ITag<TDataType> where TDataType : IDataType
    {
        private readonly TDataType _dataType;
        private readonly ITagMember<TDataType> _tagMember;
        private string _description;

        internal Tag(string name, TDataType dataType, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null, TagUsage? usage = null, bool constant = false)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            Dimensions = _dataType is IArrayType<IDataType> arrayType ? arrayType.Dimensions : Dimensions.Empty;
            Radix = radix is not null && radix.SupportsType(_dataType) ? radix : Radix.Default(_dataType);
            ExternalAccess = externalAccess ?? ExternalAccess.None;
            _description = description ?? string.Empty;
            Usage = usage != null ? usage : TagUsage.Null;
            Scope = Scope.Null;
            Constant = constant;
            Comments = new Comments(Root);

            var member = new Member<TDataType>(Name, _dataType, Radix, ExternalAccess, Description);
            _tagMember = new TagMember<TDataType>(member, Root, Parent);
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description => DetermineDescription();

        /// <inheritdoc />
        public TagName TagName => _tagMember.TagName;

        /// <inheritdoc />
        public string DataType => _tagMember.DataType;

        /// <inheritdoc />
        public Dimensions Dimensions { get; }

        /// <inheritdoc />
        public Radix Radix { get; }

        /// <inheritdoc />
        public ExternalAccess ExternalAccess { get; }

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
        public void Comment(string comment)
        {
            _description = comment;
        }

        /// <inheritdoc />
        public void SetValue(IAtomicType value) => _tagMember.SetValue(value);

        /// <inheritdoc />
        public bool TrySetValue(IAtomicType value) => _tagMember.TrySetValue(value);

        /// <inheritdoc />
        public ITagMember<TDataType> this[int x] => _tagMember[x];

        /// <inheritdoc />
        public ITagMember<TDataType> this[int x, int y] => _tagMember[x, y];

        /// <inheritdoc />
        public ITagMember<TDataType> this[int x, int y, int z] => _tagMember[x, y, z];

        /// <inheritdoc />
        public ITagMember<IDataType> this[TagName name] => _tagMember[name];

        /// <inheritdoc />
        public ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType => _tagMember.GetMember(expression);

        /// <inheritdoc />
        public IEnumerable<ITagMember<IDataType>> GetMembers() => _tagMember.GetMembers();

        /// <inheritdoc />
        public bool TryGetMember(TagName name, out ITagMember<IDataType>? tagMember) =>
            _tagMember.TryGetMember(name, out tagMember);

        /// <inheritdoc />
        public IEnumerable<TagName> GetTagNames() => _tagMember.GetTagNames();

        private string DetermineDescription()
        {
            if (!string.IsNullOrEmpty(_description)) return _description;

            if (_dataType is IUserDefined userDefined)
                return userDefined.Description;

            return string.Empty;
        }
    }
}