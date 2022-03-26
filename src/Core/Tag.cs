using System;
using System.Collections.Generic;
using L5Sharp.Creators;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public sealed class Tag<TDataType> : ITag<TDataType> where TDataType : IDataType
    {
        private readonly ITagMember<TDataType> _tagMember;

        internal Tag(string name, TDataType dataType, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null, TagUsage? usage = null, TagName? alias = null, bool constant = false,
            TagPropertyCollection<string>? comments = null, TagPropertyCollection<string>? units = null,
            TagPropertyCollection<string>? maximums = null, TagPropertyCollection<string>? minimums = null)
        {
            var root = (ITag<IDataType>)(ITag<TDataType>)this;
            var member = new Member<TDataType>(name, dataType, radix, externalAccess, description);
            _tagMember = new TagMember<TDataType>(member, root, null);

            Usage = usage ?? TagUsage.Null;
            Alias = alias ?? TagName.Empty;
            Constant = constant;
            Comments = new TagPropertyCollection<string>(comments);
            EngineeringUnits = new TagPropertyCollection<string>(units);
            //Max = new TagPropertyCollection<string>(maximumns);
            //Mins = new TagPropertyCollection<string>(minimums);

            if (!string.IsNullOrEmpty(description))
                Comments.Configure(TagName, description);
        }

        /// <summary>
        /// Creates a new <see cref="Tag{TDataType}"/> instance with the provided name and data type.
        /// </summary>
        /// <param name="name">The component name of the tag.</param>
        /// <param name="dataType">The data type instance of the tag.</param>
        /// <param name="radix">The optional radix of the tag.
        /// If not provided will use the result of <see cref="Enums.Radix.Default"/>.</param>
        /// <param name="externalAccess">The optional external access of the tag.
        /// If not provided will use <see cref="Enums.ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description">The optional description of the tag.
        /// If not provided will use <see cref="String.Empty"/>.</param>
        /// <param name="usage">The optional usage of the tag.
        /// If not provided will use <see cref="Enums.TagUsage.Null"/>.</param>
        /// <param name="alias">The optional tag name value of the alias tag.
        /// If not provided will default to <see cref="Core.TagName.Empty"/></param>
        /// <param name="constant">The optional value indicating whether the tag value is constant.
        /// If not provided will default to false.</param>
        /// <exception cref="ArgumentNullException"><c>name</c> or <c>dataType</c> are null.</exception>
        /// <exception cref="ComponentNameInvalidException">
        /// <c>name</c> does not satisfy the <see cref="ComponentName"/> constraints.
        /// </exception>
        /// <remarks>
        /// This is the primary public constructor for creating <see cref="Tag{TDataType}"/> components.
        /// For a more succinct way of creating instances of <see cref="ITag{TDataType}"/>, use the factory class
        /// <see cref="Tag"/>.
        /// </remarks>
        public Tag(ComponentName name, TDataType dataType, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null, TagUsage? usage = null, TagName? alias = null, bool constant = default)
        {
            var root = (ITag<IDataType>)(ITag<TDataType>)this;
            var member = new Member<TDataType>(name, dataType, radix, externalAccess, description);
            _tagMember = new TagMember<TDataType>(member, root, null);

            Usage = usage ?? TagUsage.Null;
            Alias = alias ?? TagName.Empty;
            Constant = constant;
            Comments = new TagPropertyCollection<string>();
            EngineeringUnits = new TagPropertyCollection<string>();

            if (!string.IsNullOrEmpty(description))
                Comments.Configure(TagName, description);
        }

        /// <inheritdoc />
        public string Name => _tagMember.Name;

        /// <inheritdoc />
        public string Description => _tagMember.Description;

        /// <inheritdoc />
        public TagName TagName => _tagMember.TagName;

        /// <inheritdoc />
        public string Units => _tagMember.Units;

        /// <inheritdoc />
        public TDataType DataType => _tagMember.DataType;

        /// <inheritdoc />
        public Dimensions Dimensions => _tagMember.Dimensions;

        /// <inheritdoc />
        public Radix Radix => _tagMember.Radix;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess => _tagMember.ExternalAccess;

        /// <inheritdoc />
        public MemberType MemberType => _tagMember.MemberType;

        /// <inheritdoc />
        public bool IsValueMember => _tagMember.IsValueMember;

        /// <inheritdoc />
        public bool IsArrayMember => _tagMember.IsArrayMember;

        /// <inheritdoc />
        public bool IsStructureMember => _tagMember.IsStructureMember;

        /// <inheritdoc />
        public bool IsStringMember => _tagMember.IsStringMember;

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
        public TagName Alias { get; }

        /// <inheritdoc />
        public bool Constant { get; }

        /// <inheritdoc />
        public TagPropertyCollection<string> Comments { get; }

        /// <inheritdoc />
        public TagPropertyCollection<string> EngineeringUnits { get; }

        /// <inheritdoc />
        public ITagMember<IDataType> this[int x] => _tagMember[x];

        /// <inheritdoc />
        public ITagMember<IDataType> this[int x, int y] => _tagMember.DataType is IArrayType<IDataType> arrayType
            ? new TagMember<IDataType>(arrayType[x, y], Root, (ITag<IDataType>)(ITag<TDataType>)this)
            : throw new InvalidOperationException(
                $"Tag of type '{GetType()}' is not an '{typeof(IArrayType<IDataType>)}'");

        /// <inheritdoc />
        public ITagMember<IDataType> this[int x, int y, int z] => _tagMember.DataType is IArrayType<IDataType> arrayType
            ? new TagMember<IDataType>(arrayType[x, y, z], Root, (ITag<IDataType>)(ITag<TDataType>)this)
            : throw new InvalidOperationException(
                $"Tag of type '{GetType()}' is not an '{typeof(IArrayType<IDataType>)}'");

        /// <inheritdoc />
        public void Comment(string comment) => _tagMember.Comment(comment);

        /// <inheritdoc />
        public bool Contains(TagName tagName) => _tagMember.Contains(tagName);

        /// <inheritdoc />
        public ITagMember<IDataType> Member(TagName tagName) => _tagMember.Member(tagName);

        /// <inheritdoc />
        public ITagMember<TType> Member<TType>(Func<TDataType, IMember<TType>> selector)
            where TType : IDataType => _tagMember.Member(selector);

        /// <inheritdoc />
        public IEnumerable<ITagMember<IDataType>> Members() => _tagMember.Members();

        /// <inheritdoc />
        public IEnumerable<ITagMember<IDataType>> Members(Func<ITagMember<IDataType>, bool> predicate) =>
            _tagMember.Members(predicate);

        /// <inheritdoc />
        public IEnumerable<TagName> TagNames() => _tagMember.TagNames();

        /// <inheritdoc />
        public bool TrySetValue(IAtomicType? value) => _tagMember.TrySetValue(value);

        /// <inheritdoc />
        public void SetValue(IAtomicType value) => _tagMember.SetValue(value);

        /// <inheritdoc />
        public void SetData(IDataType dataType) => _tagMember.SetData(dataType);
    }
}