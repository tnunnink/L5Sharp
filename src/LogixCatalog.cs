using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using Module = L5Sharp.Core.Module;

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
        private const string Type = "Type";

        private const string RockwellCatalogServiceLocalResource = @"Resources.CatalogDatabase.xml";

        private const string RockwellCatalogServiceLocalPath =
            @"Rockwell Automation\Catalog Services\CatalogSvcsDatabaseV2.xml";

        private readonly XDocument _catalog;

        /// <summary>
        /// Creates a new instance of the <see cref="LogixCatalog"/> service.
        /// </summary>
        public LogixCatalog()
        {
            var document = GetLocalCatalog();

            if (document is not null)
            {
                _catalog = document;
                return;
            }

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(RockwellCatalogServiceLocalResource);
            _catalog = XDocument.Load(stream);
        }

        /// <summary>
        /// Create a new <see cref="IModule"/> instance with the provided name and catalog number.
        /// </summary>
        /// <param name="name">The name of the Module to create.</param>
        /// <param name="catalogNumber">The catalog number of the Module being created.</param>
        /// <param name="description">The optional description of the Module being created.</param>
        /// <returns>A new <see cref="IModule"/> instance with the configured properties based on the provided catalog number.</returns>
        /// <exception cref="ArgumentException">catalogNumber was not found in the current catalog service file.</exception>
        public IModule Create(ComponentName name, CatalogNumber catalogNumber, string? description = null)
        {
            var definition = Lookup(catalogNumber);

            if (definition is null)
                throw new ArgumentException(
                    $"The provided catalog number {catalogNumber} has not module definition in the current catalog.");

            return new Module(name, definition, description);
        }

        /// <summary>
        /// Gets a <see cref="ModuleDefinition"/> instance for the provided <see cref="CatalogNumber"/>.
        /// </summary>
        /// <param name="catalogNumber">The catalog number of the <see cref="IModule"/> to lookup.</param>
        /// <returns>An <see cref="ModuleDefinition"/> instance for the specified catalogNumber if found in the current
        /// catalog service file; otherwise, null.</returns>
        /// <exception cref="ArgumentNullException">When catalogNumber is nul.l</exception>
        public ModuleDefinition? Lookup(CatalogNumber catalogNumber)
        {
            if (catalogNumber is null)
                throw new ArgumentNullException(nameof(catalogNumber));

            var device = _catalog.Descendants(RaDevice)
                .FirstOrDefault(e => e.Descendants(CatalogNumber).First().Value == catalogNumber);

            return device is not null ? MaterializeDefinition(device) : null;
        }

        /// <summary>
        /// Finds all <see cref="ModuleDefinition"/> instances having the specified <see cref="Vendor"/>.
        /// </summary>
        /// <param name="vendor">The vendor for which to get module definitions for.</param>
        /// <returns>A collection of <see cref="ModuleDefinition"/> with the specified Vendor if any exist in the current
        /// catalog; otherwise, an empty collection.</returns>
        /// <exception cref="ArgumentNullException">When vendor is null.</exception>
        public IEnumerable<ModuleDefinition> FindByVendor(Vendor vendor)
        {
            if (vendor is null)
                throw new ArgumentNullException(nameof(vendor));

            var devices = _catalog.Descendants(RaDevice)
                .Where(e => e.Descendants(VendorId).First().Value == vendor.Id.ToString());

            return devices.Select(MaterializeDefinition);
        }

        /// <summary>
        /// Fins all <see cref="ModuleDefinition"/> instances have the specified <see cref="ModuleCategory"/>.
        /// </summary>
        /// <param name="category">The category for which to get module definitions for.</param>
        /// <returns>>A collection of <see cref="ModuleDefinition"/> with the specified Category if any exist in the
        /// catalog; otherwise, an empty collection.</returns>
        /// <exception cref="ArgumentNullException">When category is null.</exception>
        public IEnumerable<ModuleDefinition> FindWithCategory(ModuleCategory category)
        {
            if (category is null)
                throw new ArgumentNullException(nameof(category));

            var devices = _catalog.Descendants(RaDevice)
                .Where(e => e.Descendants(Category).Any(c => c.Attribute(Name)?.Value == category.ToString()));

            return devices.Select(MaterializeDefinition);
        }

        private static ModuleDefinition MaterializeDefinition(XElement element)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != RaDevice)
                throw new ArgumentException(
                    $"The provided element name {element.Name} did not match the expected name {RaDevice}");

            var catalogNumber = GetCatalogNumber(element);
            var vendor = GetVendor(element);
            var productType = GetProductType(element);
            var productCode = GetProductCode(element);
            var revisions = GetRevisions(element);
            var categories = GetCategories(element);
            var ports = GetPorts(element);
            var description = GetDescription(element);

            return new ModuleDefinition(catalogNumber, vendor, productType, productCode, revisions, categories, ports,
                description);
        }

        private static CatalogNumber GetCatalogNumber(XContainer element)
        {
            return element.Descendants(CatalogNumber).FirstOrDefault()?.Value ??
                   throw new ArgumentException("The provided element does not have a CatalogNumber value");
        }

        private static Vendor GetVendor(XContainer element)
        {
            var vendor = element.Descendants(VendorId).First();

            var id = ushort.Parse(vendor.Value);
            var name = vendor.Attribute(Name)?.Value;

            return new Vendor(id, name);
        }

        private static ProductType GetProductType(XContainer element)
        {
            var productType = element.Descendants(ProductType).First();

            var id = ushort.Parse(productType.Value);
            var name = productType.Attribute(Name)?.Value;

            return new ProductType(id, name);
        }

        private static ushort GetProductCode(XContainer element)
        {
            return ushort.Parse(element.Descendants(ProductCode).First().Value);
        }

        private static IEnumerable<Revision> GetRevisions(XContainer element)
        {
            var revisions = element.Descendants(MajorRev);

            foreach (var revision in revisions)
            {
                var major = revision.Attribute(Number)?.Value;
                var minor = revision.Attribute(DefaultMinorRev)?.Value;
                //todo should add Series?

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
                var type = port.Attribute(Type)?.Value!;
                var upstream = port.Elements().All(e => e.Value != "DownstreamOnly");
                
                yield return new Port(number, type, upstream);
            }
        }

        private static string GetDescription(XContainer element)
        {
            return element.Descendants(Description).First().Value;
        }

        private static XDocument? GetLocalCatalog()
        {
            var programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var serviceFile = Path.Combine(programData, RockwellCatalogServiceLocalPath);

            var info = new FileInfo(serviceFile);

            if (!info.Exists || info.Extension != ".xml")
                return null;

            var document = XDocument.Load(info.FullName);

            //todo validate document to ensure we are loading what we think we are and that Rockwell didn't change something.

            return document;
        }
    }
}