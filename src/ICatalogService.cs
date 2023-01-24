using L5Sharp.Components;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICatalogService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="catalogNumber"></param>
        /// <returns></returns>
        ModuleDefinition Lookup(string catalogNumber);
    }
}