using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class TagMember<TDataType> : ITagMember<TDataType> where TDataType : IDataType
    {
        private readonly IMember<TDataType> _member;

        internal TagMember(IMember<TDataType> member, ITag<IDataType> root, ITagMember<IDataType>? parent)
        {
            _member = member ?? throw new ArgumentNullException(nameof(member));
            Root = root ?? throw new ArgumentNullException(nameof(root));
            Parent = parent;
        }

        /// <inheritdoc />
        public string Name => _member.Name;

        /// <inheritdoc />
        public TagName TagName => Parent is null
            ? new TagName(Name)
            : TagName.Combine(Parent.TagName, Name);

        /// <inheritdoc />
        public string DataType => _member.DataType.Name;

        /// <inheritdoc />
        public Dimensions Dimensions => _member.Dimension;

        /// <inheritdoc />
        public Radix Radix => _member.Radix;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess => Parent is null
            ? _member.ExternalAccess
            : ExternalAccess.MostRestrictive(_member.ExternalAccess, Parent.ExternalAccess);

        /// <inheritdoc />
        public string Description => Root.Comments.Contains(TagName.Operand)
            ? Root.Comments.Get(TagName.Operand)
            : CalculateDescription();

        /// <inheritdoc />
        public object? Value => _member.DataType switch
        {
            IAtomicType atomic => atomic.Value,
            IStringType stringType => stringType.Value,
            _ => null
        };

        /// <inheritdoc />
        public ITagMember<IDataType>? Parent { get; }

        /// <inheritdoc />
        public ITag<IDataType> Root { get; }

        /// <inheritdoc />
        public void Comment(string comment) => Root.Comments.Set(new Comment(TagName.Operand, comment));

        /// <inheritdoc />
        public void SetValue(IAtomicType value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            if (_member.DataType is not IAtomicType atomicType)
                throw new InvalidOperationException(
                    $"Can not set value for '{GetType()}'. Type must be atomic.");

            atomicType.SetValue(value);
        }

        /// <inheritdoc />
        public bool TrySetValue(IAtomicType value)
        {
            if (_member.DataType is not IAtomicType atomicType)
                return false;

            //todo probably need a can set or maybe use try catch?

            atomicType.SetValue(value);

            return true;
        }

        /// <inheritdoc />
        public ITagMember<TDataType> this[int x] => _member.DataType is IArrayType<TDataType> arrayType
            ? new TagMember<TDataType>(arrayType[x], Root, (ITagMember<IDataType>)this)
            : throw new InvalidOperationException();

        /// <inheritdoc />
        public ITagMember<TDataType> this[int x, int y] => _member.DataType is IArrayType<TDataType> arrayType
            ? new TagMember<TDataType>(arrayType[x, y], Root, (ITagMember<IDataType>)this)
            : throw new InvalidOperationException();

        /// <inheritdoc />
        public ITagMember<TDataType> this[int x, int y, int z]  => _member.DataType is IArrayType<TDataType> arrayType
            ? new TagMember<TDataType>(arrayType[x, y, z], Root, (ITagMember<IDataType>)this)
            : throw new InvalidOperationException();
        
        /// <inheritdoc />
        public ITagMember<IDataType> this[TagName name] => GetMemberInternal(name);

        /// <inheritdoc />
        public ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType
        {
            var parent = (ITagMember<IDataType>)this;

            var member = expression.Invoke(_member.DataType);

            return new TagMember<TType>(member, Root, parent);
        }

        /// <inheritdoc />
        public IEnumerable<ITagMember<IDataType>> GetMembers() =>
            _member.DataType.GetMembers().Select(m => new TagMember<IDataType>(m, Root, (ITagMember<IDataType>)this));

        /// <inheritdoc />
        public IEnumerable<TagName> GetTagNames() => _member.DataType.GetTagNames();

        /// <inheritdoc />
        public bool TryGetMember(TagName name, out ITagMember<IDataType>? tagMember)
        {
            throw new NotImplementedException();
        }

        private ITagMember<IDataType> GetMemberInternal(TagName name)
        {
            var members = _member.DataType.GetMembersTo(name);

            var parent = (ITagMember<IDataType>)this;

            foreach (var member in members)
            {
                var current = new TagMember<IDataType>(member, Root, parent);
                parent = current;
            }

            return parent;
        }

        /// <summary>
        /// Determines the value of the default "Pass Through Description" for the tag member.
        /// </summary>
        /// <returns>A string value of the member description</returns>
        private string CalculateDescription()
        {
            //If the parent is null this is the root member and we can just return the root's description.
            if (Parent == null) return Root.Description;

            //The rest is based on the if the root type is user defined or not.
            //If not, simply look to the parent description.
            if (typeof(IUserDefined).IsAssignableFrom(typeof(TDataType)))
                return Parent.Description;

            //If so, then we concatenate descriptions based on if it is an element of array or member of a type. 
            return Dimensions.AreEmpty
                ? $"{Root.Description} {_member.Description}".Trim()
                : $"{Root.Description} {Parent.Description}".Trim();
        }
    }
}