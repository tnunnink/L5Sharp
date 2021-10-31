using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public sealed class TagMember : TagMemberBase
    {
        private readonly ITagMember _parent;
        private readonly IMember _member;
        private string _description;

        /// <summary>
        /// Base constructor. Initialized fields if provided otherwise will opt to parent or default parameters. 
        /// </summary>
        /// <param name="parent">The parent Tag member instance</param>
        /// <param name="member"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        /// Based on testing, the following rules appear to be true:
        /// 1. The member's Data Type gets set by the data type definition. For arrays, assumes the parents type.
        /// 2. The member's Radix (Style) gets set by the data type member (but can be overridden).
        /// 3. The member's External Access is inherited from the parent/base tag
        /// 4. The member's Description (by default) is a concatenation of the parent and member description
        /// </remarks>
        internal TagMember(ITagMember parent, IMember member) : base(member.Name, member.DataType,
            member.Dimensions, member.Radix, member.ExternalAccess, member.Description, parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent), "Parent can not be null");
            _member = member;
            
            InstantiateMembers();
        }

        public override ExternalAccess ExternalAccess =>
            _member.ExternalAccess.IsMoreRestrictive(_parent.ExternalAccess)
                ? _member?.ExternalAccess
                : _parent?.ExternalAccess;

        public override string Description => string.IsNullOrEmpty(_description)
            ? $"{_parent?.Description} {_member?.Description}"
            : _description;

        public override void SetDescription(string description)
        {
            _description = description;
        }
    }
}