using System.Collections.Generic;
using System.Net;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModuleCollection : IEnumerable<IModule>
    {
        IModule? Get(ComponentName name);

        IModule? Get(string address);
        
        IModule? New(ComponentName name, CatalogNumber catalogNumber, byte slot, string? description = null,
            ICatalogService? catalogService = null);

        IModule? New(ComponentName name, CatalogNumber catalogNumber, IPAddress ipAddress, string? description = null,
            ICatalogService? catalogService = null);

        IModule? New(ComponentName name, CatalogNumber catalogNumber, byte slot, IPAddress ipAddress,
            string? description = null, ICatalogService? catalogService = null);

        IModule? New(ComponentName name, ModuleDefinition definition, string? description = null);

        bool Remove(ComponentName name);
    }
}