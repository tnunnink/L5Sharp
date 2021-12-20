using System;
using System.Collections.Generic;
using L5Sharp.Extensions;

namespace L5Sharp.Common
{
    public class StructureComparer : IEqualityComparer<IMember<IDataType>>
    {
        public bool Equals(IMember<IDataType> x, IMember<IDataType> y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return x.Name.Equals(y.Name)
                   && x.DataType.Name.Equals(y.DataType.Name)
                   && Equals(x.Dimension, y.Dimension);
            //&& x.DataType.StructureEquals(y.DataType);
        }

        public int GetHashCode(IMember<IDataType> obj)
        {
            return HashCode.Combine(obj.DataType.Name, obj.Dimension);
        }
    }
}