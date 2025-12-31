using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a definition of a module, encapsulating its catalog number, revision, vendor, product details,
/// configuration type, ports, connections, and safety-related information. This class provides functionality
/// for creating module definitions and generating new module instances based on the definition.
/// </summary>
public class ModuleDefinition : LogixElement
{
    private ModuleDefinition(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets the catalog number of the module associated with this definition.
    /// </summary>
    /// <remarks>
    /// The catalog number uniquely identifies the device within a vendor's catalog
    /// and is typically used to reference specific hardware or software components.
    /// </remarks>
    public string CatalogNumber => GetRequiredValue();

    /// <summary>
    /// Gets the revision of the module associated with this definition.
    /// </summary>
    /// <remarks>
    /// The revision represents a specific version of the module, typically used
    /// to identify hardware or firmware compatibility and distinguish between
    /// different iterations of the same product.
    /// </remarks>
    public Revision Revision => GetRevision(L5XName.Major, L5XName.Minor);

    /// <summary>
    /// Gets the vendor identifier associated with this module definition.
    /// </summary>
    /// <remarks>
    /// The vendor identifier is a unique value that represents the manufacturer or supplier
    /// of the module within the automation system.
    /// </remarks>
    public ushort Vendor => GetRequiredValue(ushort.Parse);

    /// <summary>
    /// Gets the product type identifier for the module associated with this definition.
    /// </summary>
    /// <remarks>
    /// The product type is a standardized value that classifies the module's function or category
    /// within the automation system, such as discrete input, output, or communication module.
    /// </remarks>
    public ushort ProductType => GetRequiredValue(ushort.Parse);

    /// <summary>
    /// Gets the product code of the module associated with this definition.
    /// </summary>
    /// <remarks>
    /// The product code uniquely identifies a specific product within a vendor's range of devices,
    /// distinguishing it from other products of the same type.
    /// </remarks>
    public ushort ProductCode => GetRequiredValue(ushort.Parse);

    /// <summary>
    /// Gets the collection of port types defined for the module.
    /// </summary>
    /// <remarks>
    /// Ports represent logical or physical interfaces that facilitate communication
    /// or data exchange between the module and external devices or systems.
    /// Each port type is represented as a string and defines the characteristics or functionality of the port.
    /// </remarks>
    public IEnumerable<string> Ports => GetPortTypes();

    /// <summary>
    /// Creates a new instance of <see cref="ModuleDefinition"/> using the provided <see cref="Module"/> instance.
    /// </summary>
    /// <param name="module">
    /// The <see cref="Module"/> from which to create the <see cref="ModuleDefinition"/>.
    /// It contains details such as catalog number, revision, vendor, product type, product code, and ports.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="ModuleDefinition"/> containing values extracted from the specified <see cref="Module"/>.
    /// </returns>
    public static ModuleDefinition Generate(Module module)
    {
        var xml = module.Serialize();
        var definition = GenerateDefinitionElement(xml);
        return new ModuleDefinition(definition);
    }

    /// <summary>
    /// Creates a new instance of <see cref="Module"/> based on the current <see cref="ModuleDefinition"/> and
    /// applies optional configuration.
    /// </summary>
    /// <param name="name">
    /// The name of the module to be created. This value cannot be null or empty.
    /// </param>
    /// <param name="config">
    /// An optional delegate for applying additional configuration to the newly created <see cref="Module"/> instance.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="Module"/> populated with values from the current <see cref="ModuleDefinition"/>,
    /// including ports, configuration, and connections.
    /// </returns>
    public Module Create(string name, Action<Module>? config = null)
    {
        var module = new Module(Element) { Name = name };
        config?.Invoke(module);
        return module;
    }

    /// <summary>
    /// Retrieves the types of ports defined in the module by extracting and filtering port type
    /// information from the underlying XML representation.
    /// </summary>
    private IEnumerable<string> GetPortTypes()
    {
        return Element.Descendants(L5XName.Port)
            .Select(p => p.Attribute(L5XName.Type)?.Value)
            .Where(t => t is not null)
            .Cast<string>();
    }

    /// <summary>
    /// Generates a module template in the form of an <see cref="XElement"/> based on the provided serialized module data.
    /// </summary>
    /// <param name="element">
    /// The serialized <see cref="XElement"/> representation of a module that includes attributes
    /// such as catalog number, vendor, product type, product code, and ports.
    /// </param>
    /// <returns>
    /// An <see cref="XElement"/> representing the module template, containing essential module
    /// definition attributes and child elements such as keying, ports, and communications.
    /// </returns>
    private static XElement GenerateDefinitionElement(XElement element)
    {
        var definition = new XElement(L5XName.Module);

        //Configure only the attributes we need for the module "definition" and default remaining attributes.
        definition.SetAttributeValue(L5XName.Name, string.Empty);
        definition.SetAttributeValue(L5XName.CatalogNumber, element.Attribute(L5XName.CatalogNumber)?.Value);
        definition.SetAttributeValue(L5XName.Vendor, element.Attribute(L5XName.Vendor)?.Value);
        definition.SetAttributeValue(L5XName.ProductType, element.Attribute(L5XName.ProductType)?.Value);
        definition.SetAttributeValue(L5XName.ProductCode, element.Attribute(L5XName.ProductCode)?.Value);
        definition.SetAttributeValue(L5XName.Major, element.Attribute(L5XName.Major)?.Value);
        definition.SetAttributeValue(L5XName.Minor, element.Attribute(L5XName.Minor)?.Value);
        definition.SetAttributeValue(L5XName.ParentModule, "Local");
        definition.SetAttributeValue(L5XName.ParentModPortId, 1);
        definition.SetAttributeValue(L5XName.Inhibited, false);
        definition.SetAttributeValue(L5XName.MajorFault, false);
        definition.SetAttributeValue(L5XName.SafetyEnabled, element.Attribute(L5XName.SafetyEnabled)?.Value);

        //Add the remaining child elements
        definition.Add(GenerateKeying());
        definition.Add(GeneratePorts(element.Element(L5XName.Ports)));
        definition.Add(GenerateCommunications(element.Element(L5XName.Communications)));

        return definition;
    }

    /// <summary>
    /// Generates a new electronic keying element config for the module definition. This will just default to
    /// "CompatibleModule" since that is the default in Studio.
    /// </summary>
    private static XElement GenerateKeying()
    {
        var keying = new XElement(L5XName.EKey);
        keying.SetAttributeValue(L5XName.State, ElectronicKeying.CompatibleModule);
        return keying;
    }

    /// <summary>
    /// Generates a new Ports element for the module definition element. This will copy all the configured
    /// ports for the provided element but will exclude the configured "Address" property since that is a
    /// configuration specific to the module instance.
    /// </summary>
    private static XElement? GeneratePorts(XElement? element)
    {
        if (element is null) return null;

        var ports = new XElement(L5XName.Ports);

        foreach (var child in element.Elements(L5XName.Port))
        {
            // We want to scrub the configured IP/Slot/Host and replace it with an appropriate default
            var defaultAddress =
                Address.TryParse(child.Attribute(L5XName.Address)?.Value, out var address) && address.IsNetwork
                    ? Address.NewIP()
                    : Address.NewSlot();

            var port = new XElement(L5XName.Port);
            port.SetAttributeValue(L5XName.Id, child.Attribute(L5XName.Id)?.Value);
            port.SetAttributeValue(L5XName.Type, child.Attribute(L5XName.Type)?.Value);
            port.SetAttributeValue(L5XName.Address, defaultAddress);
            port.SetAttributeValue(L5XName.Upstream, child.Attribute(L5XName.Upstream)?.Value);
            ports.Add(port);
        }

        return ports;
    }

    /// <summary>
    /// Generates a <see cref="XElement"/> representing the communications configuration of a module.
    /// </summary>
    private static XElement GenerateCommunications(XElement? element)
    {
        var communications = new XElement(L5XName.Communications);

        communications.Add(element?.Attributes());
        communications.Add(GenerateConfigTag(element?.Element(L5XName.ConfigTag)));
        communications.Add(GenerateSafetyScript(element?.Element(L5XName.SafetyScript)));

        return communications;
    }

    /// <summary>
    /// Generates a new configuration tag element based on the provided element. This will copy all attributes
    /// and child data elements, but will default the data values to effectively "scrub" the configuration.
    /// </summary>
    private static XElement? GenerateConfigTag(XElement? element)
    {
        if (element is null) return null;

        var config = new XElement(L5XName.ConfigTag);
        config.Add(element.Attributes());

        if (element.TryGetFormattedData(out var data))
        {
            //todo the question here is how do we reset the data to default values?
            config.Add(DataFormat.Format(data));
        }

        return config;
    }

    /// <summary>
    /// Generates a new safety script element using the provided element. We don't know exactly what this data represents,
    /// but it is required for safety-enabled devices to successfully import into Studio. We will just return a copy of
    /// the current element config.
    /// </summary>
    private static XElement? GenerateSafetyScript(XElement? element)
    {
        if (element is null) return null;
        return new XElement(element);
    }
}