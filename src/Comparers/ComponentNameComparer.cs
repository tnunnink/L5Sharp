using System.Collections.Generic;

namespace L5Sharp.Comparers
{
    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> that compares <see cref="ILogixComponent"/> by the string name.
    /// </summary>
    public class ComponentNameComparer : IEqualityComparer<ILogixComponent>
    {
        private ComponentNameComparer()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="ComponentNameComparer"/>.
        /// </summary>
        public static ComponentNameComparer Instance { get; } = new();


        /// <inheritdoc />
        public bool Equals(ILogixComponent? x, ILogixComponent? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return x.Name == y.Name;
        }

        /// <inheritdoc />
        public int GetHashCode(ILogixComponent obj) => obj.Name.GetHashCode();
    }
}