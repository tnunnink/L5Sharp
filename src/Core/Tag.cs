using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

[assembly: InternalsVisibleTo("L5Sharp.Core.Tests")]

namespace L5Sharp.Core
{
    public class Tag : TagBase
    {
        internal Tag(string name, IDataType dataType, IComponent parent = null, Dimensions dimensions = null,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null,
            TagUsage usage = null, bool constant = false) : base(name, dataType, dimensions, radix, externalAccess,
            description, parent, usage, constant)
        {
        }

        public override TagType TagType => TagType.Base;
    }

    public class Tag<T> : Tag, ITag<T> where T : IDataType, new()
    {
        internal Tag(string name, IComponent parent = null, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false)
            : base(name, new T(), parent, dimensions, radix, externalAccess, description, usage, constant)
        {
        }

        public ITagMember GetMember<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("");

            var propertyName = memberExpression.Member.Name;

            return Members.SingleOrDefault(m => m.Name == propertyName);
        }

        public ITag<T> As()
        {
            throw new NotImplementedException();
        }
    }
}