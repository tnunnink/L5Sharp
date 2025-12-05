using L5Sharp.Core;

namespace L5Sharp.CLI.Records;

/// <summary>
/// A record structure for the <see cref="Module"/> component.
/// </summary>
public record ModuleInfo()
{
    public ModuleInfo(Module module) : this()
    {
        Name = module.Name;
        Description = module.Description;
        CatalogNumber = module.CatalogNumber;
        Revision = module.Revision?.ToString();
        Parent = module.ParentModule;
        ProductCode = module.ProductCode;
        ProductType = module.ProductType?.Id ?? 0;
        Vendor = module.Vendor?.Id ?? 0;
        Inhibited = module.Inhibited;
        SafetyEnabled = module.SafetyEnabled;
        MajorFault = module.MajorFault;
        Slot = module.Slot ?? 0;
        IP = module.IP?.ToString();
    }

    public string Name { get; init; } = "NewModule";
    public string? Description { get; init; }
    public string? CatalogNumber { get; init; }
    public string? Revision { get; init; }
    public string? Parent { get; init; }
    public int ProductType { get; init; }
    public int ProductCode { get; init; }
    public int Vendor { get; init; }
    public bool Inhibited { get; init; }
    public bool SafetyEnabled { get; init; }
    public bool MajorFault { get; init; }

    public string? Keying { get; init; } = ElectronicKeying.CompatibleModule;
    public int Slot { get; init; }
    public string? IP { get; init; }
    
    /// <summary>
    /// Defines an implicit conversion from a <see cref="Module"/> object to a <see cref="ModuleInfo"/> object.
    /// </summary>
    /// <param name="module">The <see cref="Module"/> instance to be converted.</param>
    /// <returns>A new <see cref="ModuleInfo"/> object containing the converted program information.</returns>
    public static implicit operator ModuleInfo(Module module) => new(module);
}