namespace L5Sharp.Core
{
    /// <summary>
    /// A set of properties that defines the Logix <see cref="IModule"/>.  
    /// </summary>
    public class ModuleDefinition
    {
        
        public ModuleDefinition(CatalogNumber catalogNumber, Vendor vendor, ProductType productType, Revision revision)
        {
            CatalogNumber = catalogNumber;
            Vendor = vendor;
            ProductType = productType;
            Revision = revision;
        }
        public CatalogNumber CatalogNumber { get; }
        public Vendor Vendor { get; }

        public ProductType ProductType { get; }

        public Revision Revision { get; }
    }
}