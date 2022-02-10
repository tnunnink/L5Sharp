using L5Sharp.Core;

namespace L5Sharp
{
    public interface ICatalogService
    {
        ModuleDefinition Lookup(CatalogNumber catalogNumber);
    }
}