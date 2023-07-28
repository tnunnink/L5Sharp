using System.Xml.Linq;
using L5Sharp.Catalog;
using L5Sharp.Core;
using L5Sharp.Elements;
using L5Sharp.Enums;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Module</c> component. Contains the properties that comprise the L5X Module element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Module : LogixComponent<Module>
{
    /// <inheritdoc />
    public Module()
    {
        CatalogNumber = string.Empty;
        Vendor = Vendor.Unknown;
        ProductType = ProductType.Unknown;
        ProductCode = default;
        Revision = new Revision();
        ParentModule = string.Empty;
        ParentPortId = default;
        Inhibited = default;
        MajorFault = default;
        SafetyEnabled = default;
        Keying = ElectronicKeying.CompatibleModule;
        Ports = new LogixContainer<Port>();
        Communications = new Communications();
    }

    /// <inheritdoc />
    public Module(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    /// <remarks>
    /// Module components don't all have a name attribute (e.g. VFD peripheral modules). For this reason,
    /// the name property is overriden to not be a required field for this component type. If the name is not found,
    /// this property returns an empty string.
    /// </remarks>
    public override string Name
    {
        get => GetValue<string>() ?? string.Empty;
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the catalog number that uniquely identifies the module. This is a rockwell defined convention,
    /// and represents the identity of the module type.
    /// </summary>
    /// <value>A <see cref="string"/> value containing the catalog number.</value>
    public string? CatalogNumber
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The vendor or manufacturer of the module.
    /// </summary>
    /// <value>A <see cref="Core.Vendor"/> entity that contains the id and name of the vendor.</value>
    /// <remarks>
    /// All modules have a vendor representing the manufacturer of the module.
    /// This value can be retrieved as part of the <see cref="CatalogEntry"/> object obtained using a
    /// <see cref="ModuleCatalog"/> for catalog lookup. When deserializing from L5X file, typically only the vendor
    /// id is available on the module element.
    /// </remarks>
    public Vendor? Vendor
    {
        get => GetValue<Vendor>();
        set => SetValue(value?.Id);
    }

    /// <summary>
    /// The product type of the module, representing a category of the module.
    /// </summary>
    /// <remarks>
    /// All modules have a product type representing the product category of the module.
    /// This value can be retrieved as part of the <see cref="CatalogEntry"/> object obtained using a
    /// <see cref="ModuleCatalog"/> for catalog lookup.
    /// This value will be validated by Logix upon import of the L5X. 
    /// </remarks>
    public ProductType? ProductType
    {
        get => GetValue<ProductType>();
        set => SetValue(value?.Id);
    }

    /// <summary>
    /// The unique product code value of the module.
    /// </summary>
    /// <remarks>
    /// This is a unique value that identifies the module and is assigned by Logix.
    /// This value can be retrieved as part of the <see cref="CatalogEntry"/> object obtained using a
    /// <see cref="ModuleCatalog"/> for catalog lookup, or when deserializing from an L5X file.
    /// </remarks>
    public ushort ProductCode
    {
        get => GetValue<ushort>();
        set => SetValue(value);
    }

    /// <summary>
    /// The revision number or hardware version of the module.
    /// </summary>
    /// <value>A <see cref="Core.Revision"/> object representing the major and minor version.</value>
    /// <remarks>
    /// All modules must have a specified revision number.
    /// </remarks>
    public Revision? Revision
    {
        get
        {
            var major = Element.Attribute(L5XName.Major)?.Value;
            var minor = Element.Attribute(L5XName.Minor)?.Value;
            return major is not null && minor is not null ? Revision.Parse($"{major}.{minor}") : default;
        }
        set
        {
            Element.SetAttributeValue(L5XName.Major, value?.Major);
            Element.SetAttributeValue(L5XName.Minor, value?.Minor);
        }
    }

    /// <summary>
    /// The name of the parent module, or module that the current module is connected to upstream.
    /// This specifies how the module is connected within the module tree.
    /// </summary>
    /// <value>A <see cref="string"/> representing the parent module name. Default is an empty string.</value>
    public string? ParentModule
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The port id of the parent module that the current module is connected to.
    /// This specified how the module is connected within the module tree.
    /// </summary>
    /// <value>A <see cref="int"/> representing the id of the parent port. Default is zero.</value>
    public int ParentPortId
    {
        // ReSharper disable once ExplicitCallerInfoArgument I don't like the L5X name...
        get => GetValue<int>(L5XName.ParentModPortId);
        set => SetValue(value);
    }

    /// <summary>
    /// An indication of whether the module is inhibited or disabled.
    /// </summary>
    public bool Inhibited
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// An indication of whether the module the module will cause a major fault when faulted.
    /// </summary>
    public bool MajorFault
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// An indication of whether whether the module has safety features enabled.
    /// </summary>
    public bool SafetyEnabled
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The electronic keying mode of the module.
    /// </summary>
    /// <value>A <see cref="ElectronicKeying"/> enum value representing the mode.</value>
    public ElectronicKeying? Keying
    {
        get => Element.Element(L5XName.EKey)?.Attribute(L5XName.State)?.Value.Parse<ElectronicKeying>();
        set
        {
            if (value is null)
            {
                Element.Element(L5XName.EKey)?.Remove();
                return;
            }

            if (Element.Element(L5XName.EKey) is null)
            {
                Element.Add(new XElement(L5XName.EKey, new XAttribute(L5XName.State, value)));
                return;
            }

            Element.Element(L5XName.EKey)!.SetAttributeValue(L5XName.State, value);
        }
    }

    /// <summary>
    /// A collection of <see cref="Port"/> elements that define the module's connection within the module tree.
    /// </summary>
    /// <value>A <see cref="LogixContainer{TElement}"/> of <see cref="Port"/> objects.</value>
    /// <remarks>
    /// Ports define how a module's peripherals are connected to other module's, forming the network or tree of
    /// devices used to communicated with a controller and field equipment. Ports must have a unique id, a type,
    /// and address.
    /// </remarks>
    public LogixContainer<Port> Ports
    {
        get => GetContainer<Port>();
        set => SetContainer(value);
    }

    /// <summary>
    /// 
    /// </summary>
    public Communications? Communications
    {
        get => GetComplex<Communications>();
        set => SetComplex(value);
    }
}