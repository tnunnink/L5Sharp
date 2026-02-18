using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An <see cref="IModuleCatalog"/> implementation that accesses the locally installed Rockwell Automation catalog
/// database XML file. 
/// </summary>
/// <remarks>
/// This is the default module catalog implementation be users can provide their own for careating modules.
/// This relies on the existence of the CatalogSvcsDatabaseV2.xml file locally. This is installed with Rockwell software
/// and updated as AOP components are installed on the machine. Therefore, this represents a catalog of all the devices
/// that you should see when running Studio 5k.
/// </remarks>
public class RockwellCatalogDatabase : IModuleCatalog
{
    private const string DefaultDatabasePath = @"Rockwell Automation\Catalog Services\CatalogSvcsDatabaseV2.xml";
    private const string RaDevice = "RADevice";

    private readonly string _programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
    private readonly string _databasePath;

    /// <summary>
    /// Creates a new instance of the <see cref="RockwellCatalogDatabase"/> with the optional database file path override.
    /// </summary>
    /// <param name="databasePath">
    /// The path to the database file to read the catalog data from. If not provided, this object will default to
    /// the locally known installed file location. Override this if the file is installed else where or there
    /// is some custom version of the file.
    /// </param>
    public RockwellCatalogDatabase(string? databasePath = null)
    {
        _databasePath = databasePath ?? Path.Combine(_programData, DefaultDatabasePath);
    }

    /// <inheritdoc />
    public IEnumerable<ModuleDefinition> FindAll(string? catalogNumber = null)
    {
        var database = LoadDatabaseFile();

        var devices = database.Descendants(RaDevice)
            .Where(e =>
                e.Element("Identity")?.Element("CatalogNumber")?.Value == catalogNumber ||
                string.IsNullOrEmpty(catalogNumber)
            );

        return devices.SelectMany(GenerateDefinitionsFromDevice);
    }

    /// <inheritdoc />
    public ModuleDefinition Find(string catalogNumber, Revision revision)
    {
        if (string.IsNullOrEmpty(catalogNumber))
            throw new ArgumentException("Catalog number can not be null or empty.");

        if (revision is null)
            throw new ArgumentNullException(nameof(revision));

        var database = LoadDatabaseFile();

        var device = database
            .Descendants(RaDevice)
            .SingleOrDefault(e => e.Element("Identity")?.Element("CatalogNumber")?.Value == catalogNumber);

        var definitions = device is not null ? GenerateDefinitionsFromDevice(device) : [];
        var match = definitions.FirstOrDefault(d => d.Revision == revision);

        if (match is null)
            throw new KeyNotFoundException($"No definition found with catalog/revision: {catalogNumber}/{revision}");

        return match;
    }

    /// <inheritdoc />
    public bool TryFind(string catalogNumber, Revision revision, out ModuleDefinition definition)
    {
        if (string.IsNullOrEmpty(catalogNumber))
            throw new ArgumentException("Catalog number can not be null or empty.");

        if (revision is null)
            throw new ArgumentNullException(nameof(revision));

        var database = LoadDatabaseFile();

        var device = database
            .Descendants(RaDevice)
            .SingleOrDefault(e => e.Element("Identity")?.Element("CatalogNumber")?.Value == catalogNumber);

        var definitions = device is not null ? GenerateDefinitionsFromDevice(device) : [];
        var match = definitions.FirstOrDefault(d => d.Revision == revision);

        if (match is null)
        {
            definition = null!;
            return false;
        }

        definition = match;
        return true;
    }

    /// <inheritdoc />
    public ModuleDefinition FindLatest(string catalogNumber)
    {
        if (string.IsNullOrEmpty(catalogNumber))
            throw new ArgumentException("Catalog number can not be null or empty.");

        var database = LoadDatabaseFile();

        var device = database
            .Descendants(RaDevice)
            .SingleOrDefault(e => e.Element("Identity")?.Element("CatalogNumber")?.Value == catalogNumber);

        if (device is null)
            throw new KeyNotFoundException($"No definition found with catalog: {catalogNumber}");

        var definitions = GenerateDefinitionsFromDevice(device);
        return definitions.OrderByDescending(d => d.Revision).First();
    }

    /// <inheritdoc />
    public bool TryFindLatest(string catalogNumber, out ModuleDefinition definition)
    {
        if (string.IsNullOrEmpty(catalogNumber))
            throw new ArgumentException("Catalog number can not be null or empty.");

        var database = LoadDatabaseFile();

        var device = database
            .Descendants(RaDevice)
            .SingleOrDefault(e => e.Element("Identity")?.Element("CatalogNumber")?.Value == catalogNumber);

        if (device is null)
        {
            definition = null!;
            return false;
        }

        var definitions = GenerateDefinitionsFromDevice(device);
        definition = definitions.OrderByDescending(d => d.Revision).First();
        return true;
    }

    /// <inheritdoc />
    public IEnumerable<ModuleDefinition> FindWhere(Func<ModuleDefinition, bool> predicate)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate));

        var database = LoadDatabaseFile();

        var devices = database.Descendants(RaDevice);
        var definitions = devices.SelectMany(GenerateDefinitionsFromDevice);
        return definitions.Where(predicate);
    }

    #region Internal

    /// <summary>
    /// Generates a collection of <see cref="ModuleDefinition"/> from the provided device element. Each device can
    /// have one or more revisions, so we want to return a definition for each revision. We generate the definitions
    /// by building a virtual module instance and then returning the generated definition from that object.
    /// </summary>
    private static IEnumerable<ModuleDefinition> GenerateDefinitionsFromDevice(XElement device)
    {
        var definitions = new List<ModuleDefinition>();
        var revisions = GetRevisions(device);

        foreach (var revision in revisions)
        {
            var module = new Module
            {
                CatalogNumber = $"{GetCatalogNumber(device)}/{revision.Series}".TrimEnd('/'),
                Vendor = GetVendor(device),
                ProductType = GetProductType(device),
                ProductCode = GetProductCode(device),
                Revision = new Revision(revision.Major, revision.Minor),
                Description = GetDescription(device),
                SafetyEnabled = IsSafetyDevice(device)
            };

            module.Ports.AddRange(GeneratePorts(device));

            definitions.Add(ModuleDefinition.Generate(module));
        }

        return definitions;
    }

    /// <summary>
    /// Extracts the catalog number from the provided XML container representing a device.
    /// </summary>
    private static string GetCatalogNumber(XContainer device)
    {
        var value = device.Element("Identity")?.Element("CatalogNumber")?.Value;

        if (value is null)
            throw new InvalidOperationException(
                "CatalogNumber was not found for the catalog entry. Verify valid database file.");

        return value;
    }

    /// <summary>
    /// Extracts the vendor information from the specified XML element.
    /// </summary>
    private static ushort GetVendor(XContainer element)
    {
        var vendor = element.Element("Identity")?.Element("CIPKey")?.Element("VendorID");

        if (vendor is null)
            throw new InvalidOperationException(
                "Vendor ID was not found for the catalog entry. Verify valid database file.");

        return ushort.Parse(vendor.Value);
    }

    /// <summary>
    /// Extracts the product type information from the specified XML element.
    /// </summary>
    private static ushort GetProductType(XContainer element)
    {
        var productType = element.Element("Identity")?.Element("CIPKey")?.Element("ProductType");

        if (productType is null)
            throw new InvalidOperationException(
                "ProductType was not found for the catalog entry. Verify valid database file.");

        return ushort.Parse(productType.Value);
    }

    /// <summary>
    /// Extracts the product code from the specified XML container representing a device.
    /// </summary>
    private static ushort GetProductCode(XContainer element)
    {
        var productCode = element.Element("Identity")?.Element("CIPKey")?.Element("ProductCode")?.Value;

        if (productCode is null)
            throw new InvalidOperationException(
                "ProductCode was not found for the catalog entry. Verify valid database file.");

        return ushort.Parse(productCode);
    }

    /// <summary>
    /// Extracts a collection of revision details from the provided device XML element.
    /// </summary>
    private static IEnumerable<(ushort Major, ushort Minor, string Series)> GetRevisions(XContainer device)
    {
        var revisions = new List<(ushort Major, ushort Minor, string Series)>();

        var elements = device.Element("Identity")?.Element("CIPKey")?.Element("Revs")?.Elements("MajorRev") ?? [];

        foreach (var element in elements)
        {
            if (!element.TryGetAttribute("Number", out var number))
                throw new InvalidOperationException(
                    "Revision Number was not found for the catalog entry. Verify valid database file.");

            var major = ushort.Parse(number);
            var minor = ushort.Parse(element.Attribute("DefaultMinorRev")?.Value ?? "0");
            var series = element.Attribute("Series")?.Value ?? string.Empty;

            revisions.Add((major, minor, series));
        }

        return revisions;
    }

    /// <summary>
    /// Generates a collection of <see cref="Port"/> objects from the provided device XML element. These ports define
    /// the connections available to the module. By default, we will configure port addresses with default values and
    /// set the port connection to "upstream" unless specified as downstream only.
    /// </summary>
    private static IEnumerable<Port> GeneratePorts(XContainer device)
    {
        var ports = new List<Port>();

        var elements = device.Element(L5XName.Ports)?.Elements(L5XName.Port).ToArray() ?? [];

        foreach (var element in elements)
        {
            if (!element.TryGetAttribute(L5XName.Number, out var number))
                throw new InvalidOperationException(
                    "Port/Number was not found for the catalog entry. Ensure valid database file.");

            if (!element.TryGetAttribute(L5XName.Type, out var type))
                throw new InvalidOperationException(
                    "Port/Type was not found for the catalog entry. Ensure valid database file.");
            
            var id = int.Parse(number);
            
            // By default, populate ethernet port types with default IP and all others with default slot (0).
            var address = type == "Ethernet" ? Address.NewIP() : Address.NewSlot();
            
            // By default, make the first port the upstream port if it is not a downstream only type.
            // Most modules will have a single port which we can asssume should be upstream.
            // Modules with two ports will be configured based on how they are connected to other modules.
            // Logix requires at least one upstream port to import successfully.
            var upstream = element.Elements().All(e => e.Value != "DownstreamOnly") && id == 1;

            ports.Add(new Port
            {
                Id = id,
                Type = type,
                Address = address,
                Upstream = upstream
            });
        }

        return ports;
    }

    /// <summary>
    /// Retrieves the description from the specified device XML element.
    /// </summary>
    private static string GetDescription(XContainer device)
    {
        return device.Element("Identity")?.Elements("Description").FirstOrDefault()?.Value ?? string.Empty;
    }

    /// <summary>
    /// Determines whether the specified device belongs to the Safety category.
    /// </summary>
    private static bool IsSafetyDevice(XContainer device)
    {
        return device
            .Element("Categories")?.Elements("Category")
            .Any(c => c.Attribute("Name")?.Value == ModuleCategory.Safety) is true;
    }

    /// <summary>
    /// Loads the database file from the specified path and returns the XML document representation.
    /// </summary>
    /// <returns>An XDocument representing the loaded database file.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the catalog service file does not exist in the specified path.
    /// Ensure that Logix5000 is installed and the file is present.
    /// </exception>
    private XDocument LoadDatabaseFile()
    {
        var file = new FileInfo(_databasePath);

        if (!file.Exists)
            throw new InvalidOperationException(
                $"The catalog database file '{_databasePath}' does not exist. Ensure software is installed or provide custom file path");

        return XDocument.Load(file.FullName);
    }

    #endregion
}