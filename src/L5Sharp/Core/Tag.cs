using System;
using System.Linq;
using System.Linq.Expressions;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Tag : TagBase
    {
        public Tag(string name, IDataType dataType, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null,
            TagUsage usage = null, bool constant = false) : base(name, dataType, dimensions, radix, externalAccess,
            description, null, usage, constant)
        {
        }

        public override TagType TagType => TagType.Base;

        public object ForceValue { get; set; }

        public bool CanForce { get; set; }
    }

    public class Tag<T> : Tag where T : IDataType, new()
    {
        public Tag(string name, Dimensions dimensions = null, Radix radix = null, ExternalAccess externalAccess = null,
            string description = null, TagUsage usage = null, bool constant = false) 
            : base(name, new T(), dimensions, radix, externalAccess, description, usage, constant)
        {
        }

        public ITagMember GetMember<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("");

            var propertyName = memberExpression.Member.Name;

            return Members.SingleOrDefault(m => m.Name == propertyName);
        }
    }
}