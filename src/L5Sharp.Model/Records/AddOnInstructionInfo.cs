using L5Sharp.Core;

namespace L5Sharp.Model;

/// <summary>
/// A record structure for the <see cref="AddOnInstruction"/> component.
/// </summary>
public record AddOnInstructionInfo()
{
    public AddOnInstructionInfo(AddOnInstruction aoi) : this()
    {
        Name = aoi.Name;
        Description = aoi.Description;
        Class = aoi.Class?.Name ?? ComponentClass.Standard;
        Revision = aoi.Revision?.ToString();
        RevisionExtension = aoi.RevisionExtension;
        ExecutePreScan = aoi.ExecutePreScan;
        ExecutePostScan = aoi.ExecutePostScan;
        ExecuteEnableInFalse = aoi.ExecuteEnableInFalse;
        CreatedBy = aoi.CreatedBy;
        CreatedDate = aoi.CreatedDate.ToString("o");
        EditedBy = aoi.EditedBy;
        EditedDate = aoi.EditedDate.ToString("o");
    }

    public string Name { get; init; } = "NewAOI";
    public string? Description { get; init; }
    public string Class { get; init; } = ComponentClass.Standard;
    public string? Revision { get; init; }
    public string? RevisionExtension { get; init; }
    public bool ExecutePreScan { get; init; }
    public bool ExecutePostScan { get; init; }
    public bool ExecuteEnableInFalse { get; init; }
    public string? CreatedBy { get; init; }
    public string? CreatedDate { get; init; }
    public string? EditedBy { get; init; }
    public string? EditedDate { get; init; }

    /// <summary>
    /// Defines an implicit operator that converts an instance of <see cref="AddOnInstruction"/> to an instance of
    /// <see cref="AddOnInstructionInfo"/>.
    /// </summary>
    /// <param name="aoi">The <see cref="AddOnInstruction"/> instance to convert.</param>
    /// <returns>A new instance of <see cref="AddOnInstructionInfo"/> created from the specified <see cref="AddOnInstruction"/>.</returns>
    public static implicit operator AddOnInstructionInfo(AddOnInstruction aoi) => new(aoi);
}