using L5Sharp.Core;

namespace L5Sharp.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a catalog number is not found by a given <see cref="ICatalogService"/>.
    /// </summary>
    public class ModuleNotFoundException : LogixException
    {
        /// <summary>
        /// Creates a new <see cref="ModuleNotFoundException"/> with the provided catalog number.
        /// </summary>
        /// <param name="catalogNumber"></param>
        public ModuleNotFoundException(CatalogNumber catalogNumber) : base(
            $"The specified catalog number '{catalogNumber}' was not found in the current catalog service provider.")
        {
        }
    }
}