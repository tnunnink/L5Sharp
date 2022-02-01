using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Helpers;

namespace L5Sharp
{
    /// <summary>
    /// A service that allows lookups for <see cref="Core.ModuleDefinition"/> objects based on
    /// <see cref="Core.CatalogNumber"/> values.
    /// </summary>
    /// <remarks>
    /// This service will attempt to load the Rockwell Catalog service file into memory in order to query for data.
    /// The catalog service file's existence and content will be system dependant. If this code is run on a machine that
    /// does not have Rockwell's Studio 5000 installed, it will default to an copy embedded in the library. Note that
    /// the default embedded resource may not contain various Modules as it is dependant on installation of AOPs and EDS
    /// definitions. 
    /// </remarks>
    public class LogixCatalog
    {
        private const string RaDevice = "RADevice";
        private const string CatalogNumber = "CatalogNumber";
        private const string VendorId = "VendorID";
        private const string ProductType = "ProductType";
        private const string ProductCode = "ProductCode";
        private const string MajorRev = "MajorRev";
        private const string Category = "Category";
        private const string Port = "Port";
        private const string Name = "Name";
        private const string Description = "Description";
        private const string Number = "Number";
        private const string DefaultMinorRev = "DefaultMinorRev";
        private const string Series = "Series";
        private const string Type = "Type";

        private const string RockwellCatalogServicePath =
            @"Rockwell Automation\Catalog Services\CatalogSvcsDatabaseV2.xml";

        private readonly XDocument _catalog;

        /// <summary>
        /// Creates a new instance of the <see cref="LogixCatalog"/> service.
        /// </summary>
        public LogixCatalog()
        {
            var programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var serviceFile = Path.Combine(programData, RockwellCatalogServicePath);

            var info = new FileInfo(serviceFile);

            if (!info.Exists)
            {
                //todo use backup embedded resource?
            }

            _catalog = XDocument.Load(info.FullName);
        }

        /// <summary>
        /// Gets a <see cref="ModuleDefinition"/> instance for the provided <see cref="CatalogNumber"/>.
        /// </summary>
        /// <param name="catalogNumber">The catalog number of the <see cref="IModule"/> to lookup.</param>
        /// <returns>An <see cref="ModuleDefinition"/> instance for the specified catalogNumber if found in the current
        /// catalog service file; otherwise, null.</returns>
        /// <exception cref="ArgumentNullException">catalogNumber is null</exception>
        public ModuleDefinition? Lookup(CatalogNumber catalogNumber)
        {
            if (catalogNumber is null)
                throw new ArgumentNullException(nameof(catalogNumber));

            var device = _catalog.Descendants("RADevice")
                .FirstOrDefault(e => e.Descendants(nameof(CatalogNumber)).First().Value == catalogNumber);

            return device is not null ? MaterializeDefinition(device) : null;
        }

        /// <summary>
        /// Finds all <see cref="ModuleDefinition"/> instance with the provided <see cref="Vendor"/> id.
        /// </summary>
        /// <param name="vendor">The vendor for which to get module definitions for.</param>
        /// <returns>A collection of <see cref="ModuleDefinition"/> with the specified Vendor if any exist in the current
        /// catalog; otherwise, an empty collection.</returns>
        public IEnumerable<ModuleDefinition> FindByVendor(Vendor vendor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ModuleDefinition> FindWithCategory(ModuleCategory category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ModuleDefinition> FindWithCategies(IEnumerable<ModuleCategory> categories)
        {
            throw new NotImplementedException();
        }

        private static ModuleDefinition MaterializeDefinition(XElement element)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != RaDevice)
                throw new ArgumentException();

            var catalogNumber = GetCatalogNumber(element);
            var vendor = GetVendor(element);
            var productType = GetProductType(element);
            var productCode = GetProductCode(element);
            var revisions = GetRevisions(element);
            var categories = GetCategories(element);
            var ports = GetPorts(element);
            var description = GetDescription(element);

            return new ModuleDefinition(catalogNumber, vendor, productType, productCode, revisions, categories, ports, description);
        }

        private static CatalogNumber GetCatalogNumber(XContainer element)
        {
            return element.Descendants(CatalogNumber).FirstOrDefault()?.Value ??
                   throw new ArgumentException("The provided element does not have a CatalogNumber value");
        }

        private static Vendor GetVendor(XContainer element)
        {
            var vendor = element.Descendants(VendorId).FirstOrDefault();

            if (vendor is null)
                throw new InvalidOperationException("The device element did not have a valid Vendor");

            var id = ushort.Parse(vendor.Value);
            var name = vendor.Attribute(Name)?.Value;

            return new Vendor(id, name);
        }

        private static ProductType GetProductType(XContainer element)
        {
            var productType = element.Descendants(ProductType).FirstOrDefault();

            if (productType is null)
                throw new InvalidOperationException("The device element did not have a valid Product Type");
            
            var id = ushort.Parse(productType.Value);
            var name = productType.Attribute(Name)?.Value;

            return new ProductType(id, name);
        }

        private static ushort GetProductCode(XContainer element)
        {
            var productCode = element.Descendants(ProductCode).FirstOrDefault();
            
            if (productCode is null)
                throw new InvalidOperationException("The device element did not have a valid Product Code");

            return ushort.Parse(productCode.Value);
        }
        
        
        private static IEnumerable<Revision> GetRevisions(XContainer element)
        {
            var revisions = element.Descendants(MajorRev);

            foreach (var category in revisions)
            {
                var major = category.Attribute(Number)?.Value;
                var minor = category.Attribute(DefaultMinorRev)?.Value;

                yield return Revision.Parse($"{major}.{minor}");
            }
        }

        private static IEnumerable<ModuleCategory> GetCategories(XContainer element)
        {
            var categories = element.Descendants(Category);

            foreach (var category in categories)
            {
                var name = category.Attribute(Name)?.Value;
                
                if (ModuleCategory.TryFromName(name, out var moduleCategory))
                    yield return moduleCategory;
            }
        }

        private static IEnumerable<Port> GetPorts(XContainer element)
        {
            var ports = element.Descendants(Port);

            foreach (var port in ports)
            {
                var number = int.Parse(port.Attribute(Number)?.Value!);
                var type = port.Attribute(Type)?.Value;

                yield return new Port(number, "", type!);
            }
        }

        private static string GetDescription(XContainer element)
        {
            return element.Descendants(LogixNames.Description).FirstOrDefault()?.Value ?? string.Empty;
        }
    }
}