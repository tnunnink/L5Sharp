using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;

namespace L5Sharp.Utilities
{
    /// <summary>
    /// A <see cref="IEqualityComparer{T}"/> for the <see cref="TagName"/> object.
    /// </summary>
    public class TagNameComparer : IEqualityComparer<TagName>
    {
        private TagNameComparer()
        {
        }

        /// <summary>
        /// An <see cref="IEqualityComparer{T}"/> that compares the full <see cref="TagName"/> value.
        /// </summary>
        public static TagNameComparer FullName { get; } = new();

        /// <summary>
        /// An <see cref="IEqualityComparer{T}"/> that compares the <see cref="TagName.Base"/> property of the
        /// <see cref="TagName"/> value.
        /// </summary>
        public static TagNameComparer BaseName { get; } = new BaseTagNameComparer();

        /// <summary>
        /// An <see cref="IEqualityComparer{T}"/> that compares the <see cref="TagName.Path"/> property of the
        /// <see cref="TagName"/> value.
        /// </summary>
        public static TagNameComparer PathName { get; } = new PathTagNameComparer();
        
        /// <summary>
        /// An <see cref="IEqualityComparer{T}"/> that compares the last element of the <see cref="TagName.Members"/>
        /// property of the <see cref="TagName"/> value.
        /// </summary>
        public static TagNameComparer MemberName { get; } = new MemberTagNameComparer();

        /// <inheritdoc />
        public virtual bool Equals(TagName? x, TagName? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
            return x.Equals(y);
        }

        /// <inheritdoc />
        public virtual int GetHashCode(TagName obj) => obj.GetHashCode();


        private class BaseTagNameComparer : TagNameComparer
        {
            public override bool Equals(TagName? x, TagName? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
                return string.Equals(x.Base, y.Base, StringComparison.OrdinalIgnoreCase);
            }

            public override int GetHashCode(TagName obj) => StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Base);
        }
        
        private class PathTagNameComparer : TagNameComparer
        {
            public override bool Equals(TagName? x, TagName? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
                return string.Equals(x.Path, y.Path, StringComparison.OrdinalIgnoreCase);
            }

            public override int GetHashCode(TagName obj) => StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Path);
        }
        
        private class MemberTagNameComparer : TagNameComparer
        {
            public override bool Equals(TagName? x, TagName? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
                return string.Equals(x.Members.Last(), y.Members.Last(), StringComparison.OrdinalIgnoreCase);
            }

            public override int GetHashCode(TagName obj) => StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Members.Last());
        }
    }
}