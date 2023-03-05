using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Rockwell;

namespace L5Sharp
{
    /// <summary>
    /// A service for providing <see cref="CatalogEntry"/> data for a specified catalog number.
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public interface ICatalogService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="catalogNumber"></param>
        /// <returns></returns>
        CatalogEntry Lookup(string catalogNumber);
    }
}