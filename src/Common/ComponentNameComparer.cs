using System;
using System.Collections.Generic;

namespace L5Sharp.Common
{
    /// <summary>
    /// Custom equality comparer that compares Logix components by name only.
    /// </summary>
    /// <typeparam name="TComponent">The type of Logix component.</typeparam>
    public class ComponentNameComparer<TComponent> : IEqualityComparer<TComponent> 
        where TComponent : ILogixComponent
    {
        /// <inheritdoc />
        public bool Equals(TComponent? x, TComponent? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Name == y.Name;
        }

        /// <inheritdoc />
        public int GetHashCode(TComponent obj)
        {
            return HashCode.Combine(obj.Name);
        }
    }
}