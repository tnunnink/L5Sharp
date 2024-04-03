using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A class containing information regarding the L5X export file. This information is found on the root
/// RSLogix5000Content element, and is used by the Logix software to determine the context of the L5X file.
/// </summary>
public class L5XInfo : LogixElement
{
    internal L5XInfo(XElement element) : base(element)
    {
    }
    
    /// <summary>
    /// Gets the value of the schema revision for the current L5X content.
    /// </summary>
    /// <value>A <see cref="Revision"/> type that represent the major/minor revision of the L5X schema.</value>
    /// <remarks>This is always 1.0. If the R</remarks>
    public Revision? SchemaRevision
    {
        get => GetValue<Revision>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the value of the software revision for the current L5X content.
    /// </summary>
    /// <value>A <see cref="Revision"/> type that represent the major/minor revision of the software.</value>
    public Revision? SoftwareRevision
    {
        get => GetValue<Revision>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the name of the Logix component that is the target of the current L5X context.
    /// </summary>
    public string? TargetName
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the type of Logix component that is the target of the current L5X context.
    /// </summary>
    public string? TargetType
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the value indicating whether the current L5X is contextual..
    /// </summary>
    public bool? ContainsContext
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the owner that exported the current L5X file.
    /// </summary>
    public string? Owner => GetValue<string>();

    /// <summary>
    /// Gets the date time that the L5X file was exported.
    /// </summary>
    public DateTime? ExportDate => GetDateTime();

    /// <summary>
    /// Gets the set of configured export options for the L5X file.
    /// </summary>
    /// <value>A collection of <see cref="string"/> indicating the option values.</value>
    public IEnumerable<string> ExportOptions => GetValues<string>();
}