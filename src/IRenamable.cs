using System;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// Provided the ability to rename a component.
    /// </summary>
    public interface IRenamable
    {
        /// <summary>
        /// Renames the current <see cref="ILogixComponent"/> to the provided component name.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> value.</param>
        /// <returns>A new identical <see cref="ILogixComponent"/> with the updated name.</returns>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        ILogixComponent Rename(ComponentName name);
    }
}