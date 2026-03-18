using System.Xml.Linq;
using L5Sharp.Core;

namespace L5Sharp.Catalog.Internal;

/// <summary>
/// Provides functionality to read and parse module definitions from an RA database file. This class is responsible for
/// extracting definitions of all available devices in the database, represented as <see cref="ModuleDefinition"/>
/// instances.
/// </summary>
public static class RockwellDatabaseReader
{
    /// <summary>
    /// Reads module definitions from the specified RA database file and returns a collection of
    /// <see cref="ModuleDefinition"/> representing each module in the database. The method loads
    /// the database file, parses its contents, and generates definitions for all available devices
    /// found within the file.
    /// </summary>
    /// <param name="filePath">The full path to the RA database file from which to read the module definitions.</param>
    /// <returns>A collection of <see cref="ModuleDefinition"/> containing the data for each module defined in the database.</returns>
    public static IEnumerable<ModuleDefinition> ReadDefinitions(string filePath)
    {
        var database = LoadDatabaseFile(filePath);
        var definitions = database.Descendants("RADevice").SelectMany(GenerateDefinitionsFromDevice);
        return definitions;
    }

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
            var address = type == "Ethernet" ? Address.NewIP() : Address.NewSlot();

            // By default, make the first port the upstream port if it is not a downstream only type.
            // Most modules will have a single port which we can assume should be upstream.
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
    private static XDocument LoadDatabaseFile(string databasePath)
    {
        var file = new FileInfo(databasePath);

        if (!file.Exists)
            throw new FileNotFoundException($"The catalog database file '{databasePath}' does not exist.");

        return XDocument.Load(file.FullName);
    }
}