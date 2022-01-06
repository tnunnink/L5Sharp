using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Extensions;

namespace L5Sharp.Comparers
{
    /// <summary>
    /// 
    /// </summary>
    public class DataTypeStructureComparer : IEqualityComparer<IDataType>
    {
        private DataTypeStructureComparer()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="ComponentNameComparer"/>.
        /// </summary>
        public static DataTypeStructureComparer Instance { get; } = new();
        
        /// <inheritdoc />
        public bool Equals(IDataType? x, IDataType? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return x.Name == y.Name
                   && x.GetMembers().SequenceEqual(y.GetMembers(), MemberStructureComparer.Instance);
        }

        /// <inheritdoc />
        public int GetHashCode(IDataType obj)
        {
            throw new NotImplementedException();
        }
    }
}