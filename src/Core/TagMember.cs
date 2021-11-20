using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class TagMember<TDataType> : ITagMember<TDataType> where TDataType : IDataType
    {
        private readonly IMember<TDataType> _member;

        internal TagMember(IMember<TDataType> member, TagMember<IDataType> parent, Tag<IDataType> root)
        {
            _member = member ?? throw new ArgumentNullException(nameof(member));

            Parent = parent;
            Root = root;
        }

        /// <inheritdoc />
        public string Name => _member.Name;

        /// <inheritdoc />
        public string TagName => Parent == null
            ? Name
            : _member.IsArrayElement()
                ? $"{GetName(Parent)}{Name}"
                : $"{GetName(Parent)}.{Name}";

        /// <inheritdoc />
        public string DataType => _member.DataType.Name;

        /// <inheritdoc />
        public Dimensions Dimensions => _member.Dimension;

        /// <inheritdoc />
        public Radix Radix => _member.DataType.Radix;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess =>
            Parent == null
                ? _member.ExternalAccess
                : _member.ExternalAccess.IsMoreRestrictive(Parent.ExternalAccess)
                    ? _member.ExternalAccess
                    : Parent.ExternalAccess;

        /// <inheritdoc />
        public string Description => _member.Description;


        private Tag<IDataType> Root { get; }
        private TagMember<IDataType> Parent { get; }

        /// <inheritdoc />
        public IAtomic GetData()
        {
            return _member.DataType is IAtomic atomic ? atomic : default;
        }

        /// <inheritdoc />
        public void SetData(IAtomic value)
        {
            if (!(_member.DataType is IAtomic atomic))
                throw new InvalidTagDataException(_member.DataType, value);

            atomic.SetValue(value);
        }

        /// <inheritdoc />
        public void SetRadix(Radix radix)
        {
            if (radix == null)
                throw new ArgumentNullException(nameof(radix));

            if (!(_member.DataType is IAtomic atomic))
                throw new InvalidOperationException(
                    $"{_member.DataType.Name} is not Atomic. Radix can only be set on Atomic types");

            atomic.SetRadix(radix);
        }

        /// <inheritdoc />
        public void SetDescription(string description)
        {
        }

        /// <inheritdoc />
        public IEnumerable<string> GetMemberNames() => _member.DataType.GetMembers().Select(m => m.Name.ToString());

        /// <inheritdoc />
        public IEnumerable<string> GetDeepMembersNames() => _member.DataType.GetMemberNames();

        /// <inheritdoc />
        public ITagMember<IDataType> this[string name] => GetMember(name);

        /// <inheritdoc />
        public ITagMember<IDataType> this[Func<TDataType, IMember<IDataType>> expression] => GetMember(expression);

        /// <inheritdoc />
        public ITagMember<TDataType> this[int index] => GetMember(index);

        /// <inheritdoc />
        public IEnumerable<ITagMember<IDataType>> GetMembers() => GetMembersInternal();

        /// <inheritdoc />
        public IEnumerable<ITagMember<IDataType>> GetMembers(Func<ITagMember<IDataType>, bool> predicate) =>
            GetMembersInternal().Where(predicate);

        /// <inheritdoc />
        public ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType
        {
            var member = expression.Invoke(_member.DataType);
            return member != null ? new TagMember<TType>(member, this as TagMember<IDataType>, Root) : null;
        }

        /// <inheritdoc />
        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, TAtomic value)
            where TAtomic : IAtomic
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Expression can not be null");

            var member = expression.Invoke(_member.DataType);
            member.DataType.SetValue(value);
        }

        /// <inheritdoc />
        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, Radix radix)
            where TAtomic : IAtomic
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Expression can not be null");

            var member = expression.Invoke(_member.DataType);
            member.DataType.SetRadix(radix);
        }

        /// <inheritdoc />
        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, string description)
            where TAtomic : IAtomic
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Expression can not be null");

            var member = expression.Invoke(_member.DataType);
        }

        private IEnumerable<ITagMember<IDataType>> GetMembersInternal()
        {
            if (_member is IArrayMember<TDataType> arrayMember)
            {
                return arrayMember.Select(e =>
                    new TagMember<IDataType>((IMember<IDataType>)e, this as TagMember<IDataType>, Root));
            }

            return _member.DataType.GetMembers()
                .Select(m => new TagMember<IDataType>(m, this as TagMember<IDataType>, Root));
        }

        private ITagMember<IDataType> GetMember(string name)
        {
            var member = _member.DataType.GetMember(name);
            return member != null ? new TagMember<IDataType>(member, this as TagMember<IDataType>, Root) : null;
        }

        private ITagMember<TDataType> GetMember(int index)
        {
            return _member is IArrayMember<TDataType> arrayMember
                ? new TagMember<TDataType>(arrayMember[index], this as TagMember<IDataType>, Root)
                : null;
        }

        private static string GetName(TagMember<IDataType> member)
        {
            return member.Parent != null
                ? member.Parent.Dimensions.Length > 0
                    ? $"{GetName(member.Parent)}{member.Name}"
                    : $"{GetName(member.Parent)}.{member.Name}"
                : member.Name;
        }
    }
}