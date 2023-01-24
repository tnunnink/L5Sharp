using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Rockwell
{
    /// <summary>
    /// A service that allows lookups for <see cref="Core.ModuleDefinition"/> objects based on catalog numbers.
    /// </summary>
    /// <remarks>
    /// This service will attempt to load the Rockwell Catalog service file into memory in order to query for data.
    /// The catalog service file's existence and content will be system dependant. If this code is run on a machine that
    /// does not have Rockwell's Studio 5000 installed, it will default to an copy embedded in the library. Note that
    /// the default embedded resource may not contain various Modules as it is dependant on installation of AOPs and EDS
    /// definitions. 
    /// </remarks>
    public class ModuleCatalog : ICatalogService
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
        private const string DownstreamOnly = "DownstreamOnly";

        private const string RockwellCatalogServiceLocalPath =
            @"Rockwell Automation\Catalog Services\CatalogSvcsDatabaseV2.xml";

        private readonly XDocument _catalog;

        /// <summary>
        /// Creates a new instance of the <see cref="ModuleCatalog"/> service.
        /// </summary>
        public ModuleCatalog()
        {
            _catalog = GetLocalCatalog();
        }

        /// <summary>
        /// Gets a <see cref="ModuleDefinition"/> instance for the provided <see cref="CatalogNumber"/>.
        /// </summary>
        /// <param name="catalogNumber">The catalog number of the <see cref="Module"/> to lookup.</param>
        /// <returns>A <see cref="ModuleDefinition"/> instance for the specified catalogNumber if found in the current
        /// catalog service file; otherwise, null.</returns>
        /// <exception cref="ArgumentNullException"><c>catalogNumber</c> is null.</exception>
        /// <exception cref="InvalidOperationException">The provided catalog number was not found.</exception>
        public ModuleDefinition Lookup(string catalogNumber)
        {
            if (catalogNumber is null)
                throw new ArgumentNullException(nameof(catalogNumber));

            var device = _catalog.Descendants(RaDevice)
                .FirstOrDefault(e => e.Descendants(CatalogNumber).First().Value == catalogNumber);

            if (device is null)
                throw new InvalidOperationException(
                    $"Device with catalog number {catalogNumber} does not exists in local catalog database.");

            return MaterializeDefinition(device);
        }

        private static ModuleDefinition MaterializeDefinition(XContainer element)
        {
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

        private static string GetCatalogNumber(XContainer element) =>
            element.Descendants(CatalogNumber).FirstOrDefault()!.Value;

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

        private static ushort GetProductCode(XContainer element) =>
            ushort.Parse(element.Descendants(ProductCode).First().Value);

        private static IEnumerable<Revision> GetRevisions(XContainer element)
        {
            var revisions = element.Descendants(MajorRev);

            foreach (var revision in revisions)
            {
                var major = revision.Attribute(Number)?.Value;
                var minor = revision.Attribute(DefaultMinorRev)?.Value;

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
                var downstreamOnly = port.Elements().Any(e => e.Value == DownstreamOnly);

                yield return new Port
                {
                    Id = number,
                    Type = type,
                    DownstreamOnly = downstreamOnly
                };
            }
        }

        private static string GetDescription(XContainer element) =>
            element.Descendants(Description).First().Value;

        private static XDocument GetLocalCatalog()
        {
            var programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var serviceFile = Path.Combine(programData, RockwellCatalogServiceLocalPath);

            var info = new FileInfo(serviceFile);

            if (!info.Exists)
                throw new ArgumentException(
                    $"The catalog service file {serviceFile} does not exist in the current environment.");

            var document = XDocument.Load(info.FullName);

            return document;
        }
    }
}