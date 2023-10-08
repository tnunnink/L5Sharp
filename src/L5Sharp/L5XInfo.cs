using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Utilities;

namespace L5Sharp;

/// <summary>
/// A class containing information regarding the L5X export file. This information is found on the root
/// RSLogix5000Content element, and is used by the Logix software to determine the context of the L5X file.
/// </summary>
public class L5XInfo
{
    private readonly XElement _element;

    internal L5XInfo(XElement element)
    {
        _element = element;
    }
    
    /// <summary>
    /// Gets the value of the schema revision for the current L5X content.
    /// </summary>
    /// <value>A <see cref="Revision"/> type that represent the major/minor revision of the L5X schema.</value>
    /// <remarks>This is always 1.0. If the R</remarks>
    public Revision? SchemaRevision
    {
        get => _element.Attribute(L5XName.SchemaRevision)?.Value.Parse<Revision>();
        set => _element.SetAttributeValue(L5XName.SchemaRevision, value);
    }

    /// <summary>
    /// Gets the value of the software revision for the current L5X content.
    /// </summary>
    /// <value>A <see cref="Revision"/> type that represent the major/minor revision of the software.</value>
    public Revision? SoftwareRevision
    {
        get => _element.Attribute(L5XName.SoftwareRevision)?.Value.Parse<Revision>();
        set => _element.SetAttributeValue(L5XName.SoftwareRevision, value);
    }

    /// <summary>
    /// Gets the name of the Logix component that is the target of the current L5X context.
    /// </summary>
    public string? TargetName
    {
        get => _element.Attribute(L5XName.TargetName)?.Value;
        set => _element.SetAttributeValue(L5XName.TargetName, value);
    }

    /// <summary>
    /// Gets the type of Logix component that is the target of the current L5X context.
    /// </summary>
    public string? TargetType
    {
        get => _element.Attribute(L5XName.TargetType)?.Value;
        set => _element.SetAttributeValue(L5XName.TargetType, value);
    }

    /// <summary>
    /// Gets the value indicating whether the current L5X is contextual..
    /// </summary>
    public bool? ContainsContext
    {
        get => _element.Attribute(L5XName.ContainsContext)?.Value.Parse<bool>();
        set => _element.SetAttributeValue(L5XName.ContainsContext, value);
    }

    /// <summary>
    /// Gets the owner that exported the current L5X file.
    /// </summary>
    public string? Owner
    {
        get => _element.Attribute(L5XName.Owner)?.Value;
        set => _element.SetAttributeValue(L5XName.Owner, value);
    }

    /// <summary>
    /// Gets the date time that the L5X file was exported.
    /// </summary>
    public DateTime? ExportDate => _element.Attribute(L5XName.ExportDate)?.Value is not null
        ? DateTime.ParseExact(_element.Attribute(L5XName.ExportDate)?.Value, L5X.DateTimeFormat, null)
        : default;
}