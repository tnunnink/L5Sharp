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

        internal TagMember(IMember<TDataType> member, ITagMember<IDataType> parent, ITag<IDataType> baseTag)
        {
            _member = member ?? throw new ArgumentNullException(nameof(member));
            Base = baseTag ?? throw new ArgumentNullException(nameof(baseTag));
            Parent = parent;
        }

        /// <inheritdoc />
        public string Name => _member.Name;

        /// <inheritdoc />
        public string TagName => GetTagName((ITagMember<IDataType>)this);

        /// <inheritdoc />
        public string Operand => GetTagName((ITagMember<IDataType>)this).Replace(Base.Name, string.Empty);

        /// <inheritdoc />
        public string DataType => _member.DataType.Name;

        /// <inheritdoc />
        public Dimensions Dimensions => _member.Dimension;

        /// <inheritdoc />
        public Radix Radix => _member.Radix;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess =>
            Parent == null
                ? _member.ExternalAccess
                : _member.ExternalAccess.IsMoreRestrictive(Parent.ExternalAccess)
                    ? _member.ExternalAccess
                    : Parent.ExternalAccess;

        /// <inheritdoc />
        public string Description => Base.Comments.HasComment(Operand)
            ? Base.Comments.GetComment(Operand)
            : CalculateDescription();

        /// <inheritdoc />
        public ITagMember<IDataType> Parent { get; }

        private ITag<IDataType> Base { get; }

        /// <inheritdoc />
        public IAtomic GetData()
        {
            return _member.DataType is IAtomic atomic ? atomic : default;
        }

        /// <inheritdoc />
        public void SetData(IAtomic value)
        {
            if (_member.DataType is not IAtomic atomic)
                throw new InvalidTagDataException(_member.DataType, value);

            atomic.Update(value);
        }

        /*/// <inheritdoc />
        public void SetRadix(Radix radix)
        {
            if (radix == null)
                throw new ArgumentNullException(nameof(radix));

            if (!(_member.DataType is IAtomic atomic))
                throw new InvalidOperationException(
                    $"{_member.DataType.Name} is not Atomic. Radix can only be set on Atomic types");

            atomic.SetRadix(radix);

            if (!(_member is IArrayMember<IAtomic> array)) return;

            foreach (var element in array)
                element.DataType.SetRadix(radix);
        }*/

        /// <inheritdoc />
        public void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                Base.Comments.Reset(Operand);
                return;
            }

            Base.Comments.Override(new Comment(Operand, description));
        }

        /// <inheritdoc />
        public IEnumerable<string> GetMemberNames() => _member.GetMemberNames();

        /// <inheritdoc />
        public IEnumerable<string> GetDeepMembersNames() => _member.GetDeepMemberNames();

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
            return member != null ? new TagMember<TType>(member, (ITagMember<IDataType>)this, Base) : null;
        }

        /// <inheritdoc />
        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, TAtomic value)
            where TAtomic : IAtomic
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Expression can not be null");

            var member = expression.Invoke(_member.DataType);
            member.DataType.Update(value);
        }

        /*/// <inheritdoc />
        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, Radix radix)
            where TAtomic : IAtomic
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Expression can not be null");

            var member = expression.Invoke(_member.DataType);
            member.DataType.SetRadix(radix);

            if (member is not IArrayMember<IAtomic> array) return;

            foreach (var element in array)
                element.DataType.SetRadix(radix);
        }*/

        /// <inheritdoc />
        public void SetMember<TType>(Func<TDataType, IMember<TType>> expression, string description)
            where TType : IDataType
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Expression can not be null");

            var member = expression.Invoke(_member.DataType);

            var operand = member is IArrayMember<IDataType> ? member.Name : $".{member.Name}";

            if (string.IsNullOrEmpty(description))
            {
                Base.Comments.Reset(operand);
                return;
            }

            Base.Comments.Override(new Comment(operand, description));
        }

        private IEnumerable<ITagMember<IDataType>> GetMembersInternal()
        {
            if (_member is IArrayMember<TDataType> arrayMember)
            {
                return arrayMember.Select(e =>
                    new TagMember<IDataType>((IMember<IDataType>)e, (ITagMember<IDataType>)this, Base));
            }

            return _member.DataType.GetMembers()
                .Select(m => new TagMember<IDataType>(m, (ITagMember<IDataType>)this, Base));
        }

        private ITagMember<IDataType> GetMember(string name)
        {
            var member = _member.DataType.GetMember(name);
            return member != null ? new TagMember<IDataType>(member, (ITagMember<IDataType>)this, Base) : null;
        }

        private ITagMember<TDataType> GetMember(int index)
        {
            return _member is IArrayMember<TDataType> arrayMember && index >= 0 && index < Dimensions.Length
                ? new TagMember<TDataType>(arrayMember[index], (ITagMember<IDataType>)this, Base)
                : null;
        }

        /// <summary>
        /// Recursively traverses up the member chain to build the full string name of the current tag member. 
        /// </summary>
        /// <param name="member">The current member to evaluate.</param>
        /// <returns>The full string tag name of the the tag member.</returns>
        private static string GetTagName(ITagMember<IDataType> member)
        {
            return member.Parent != null
                ? member.IsArrayElement()
                    ? $"{GetTagName(member.Parent)}{member.Name}"
                    : $"{GetTagName(member.Parent)}.{member.Name}"
                : member.Name;
        }

        /// <summary>
        /// Determines the value of the default "Pass Through Description" for the tag member.
        /// </summary>
        /// <returns>A string value of the member description</returns>
        private string CalculateDescription()
        {
            //If the parent is null this is the root member and we can just return the root's description.
            if (Parent == null) return Base.Description;

            //The rest is based on the if the root type is user defined or not.
            //If not, simply look to the parent description.
            if (typeof(IUserDefined).IsAssignableFrom(typeof(TDataType)))
                return Parent.Description;

            //If so, then we concatenate descriptions based on if it is an element of array or member of a type. 
            return this.IsArrayElement()
                ? $"{Base.Description} {Parent.Description}".Trim()
                : $"{Base.Description} {_member.Description}".Trim();
        }
    }
}