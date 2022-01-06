using System.Collections.Generic;
using L5Sharp.Extensions;

namespace L5Sharp.Comparers
{
    /// <summary>
    /// 
    /// </summary>
    public class MemberStructureComparer : EqualityComparer<IMember<IDataType>>
    {
        private MemberStructureComparer()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="MemberStructureComparer"/>.
        /// </summary>
        public static MemberStructureComparer Instance { get; } = new();
        
        /// <inheritdoc />
        public override bool Equals(IMember<IDataType>? x, IMember<IDataType>? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return x.Name.Equals(y.Name) &&
                   x.DataType.StructureEquals(y.DataType) &&
                   x.Dimension.Equals(y.Dimension);
        }

        /// <inheritdoc />
        public override int GetHashCode(IMember<IDataType> obj) =>
            obj.Name.GetHashCode() ^ obj.DataType.GetHashCode() ^ obj.Dimension.GetHashCode();
    }
}