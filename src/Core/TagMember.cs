using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

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
        public string TagName => GetTagName((ITagMember<IDataType>)this);

        /// <inheritdoc />
        public string Operand => GetTagName((ITagMember<IDataType>)this).Replace(Root.Name, string.Empty);

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
        public string Description => Root.Comments.Contains(Operand)
            ? Root.Comments.Get(Operand)
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
        public void Comment(string comment) => Root.Comments.Set(new Comment(Operand, comment));

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
        public ITagMember<TDataType>? this[int index] => throw new NotImplementedException();

        /// <inheritdoc />
        public ITagMember<IDataType>? this[string name] => throw new NotImplementedException();

        /// <inheritdoc />
        public ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType
        {
            var member = expression.Invoke(_member.DataType);
            return new TagMember<TType>(member, Root, (ITagMember<IDataType>)this);
        }

        /// <inheritdoc />
        public IEnumerable<ITagMember<IDataType>> GetMembers() => _member.DataType is IComplexType complexType
            ? complexType.Members.Select(m => new TagMember<IDataType>(m, Root, (ITagMember<IDataType>)this))
            : Enumerable.Empty<ITagMember<IDataType>>();

        /// <inheritdoc />
        public IEnumerable<string> GetMemberNames() => _member.DataType is IComplexType complexType
            ? complexType.Members.Select(m => m.Name)
            : Enumerable.Empty<string>();

        /// <inheritdoc />
        public IEnumerable<string> GetDeepMembersNames() => throw new NotImplementedException();

        /// <summary>
        /// Recursively traverses up the member chain to build the full string name of the current tag member. 
        /// </summary>
        /// <param name="member">The current member to evaluate.</param>
        /// <returns>The full string tag name of the the tag member.</returns>
        private static string GetTagName(ITagMember<IDataType> member)
        {
            return member.Parent != null
                ? member.Parent.Dimensions.AreEmpty
                    ? $"{GetTagName(member.Parent)}.{member.Name}"
                    : $"{GetTagName(member.Parent)}{member.Name}"
                : member.Name;
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