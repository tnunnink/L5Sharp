using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp;

/// <summary>
/// A <see cref="XElement"/> decorator that adds members for interacting with the root L5X content.
/// </summary>
/// <remarks>
/// Most of these methods and properties are meant to be internal and are referenced when mutating or querying the content
/// of an L5X file. However, the user can use this type to access the XML directly to extend or query the data.
/// </remarks>
public class L5X : XElement
{
    /// <summary>
    /// The date/time format for the L5X content.
    /// </summary>
    public const string DateTimeFormat = "ddd MMM d HH:mm:ss yyyy";

    /// <summary>
    /// The list of top level component containers for a L5X content or controller element. 
    /// </summary>
    private static readonly List<XName> Containers = new()
    {
        L5XName.DataTypes,
        L5XName.Modules,
        L5XName.AddOnInstructionDefinitions,
        L5XName.Tags,
        L5XName.Programs,
        L5XName.Tasks,
        L5XName.ParameterConnections,
        L5XName.Trends,
        L5XName.QuickWatchLists
    };

    /// <summary>
    /// Creates a new <see cref="L5X"/> instance wrapping the provided <see cref="XElement"/> object.
    /// </summary>
    /// <param name="content"></param>
    public L5X(XElement content) : base(content)
    {
        if (content is null)
            throw new ArgumentNullException(nameof(content));

        if (content.Name != L5XName.RSLogix5000Content)
            throw new ArgumentException($"Expecting root element name of {L5XName.RSLogix5000Content}.");

        // We will "normalize" (ensure consistent root controller element and component containers) for all
        // files containing context (files that are not full projects) so that we won't have issues mutating the L5X.
        if (ContainsContext is true || content.Element(L5XName.Controller) is null)
            Normalize(this);
    }

    /// <summary>
    /// Gets the value of the schema revision for the current L5X content.
    /// </summary>
    /// <value>A <see cref="Revision"/> type that represent the major/minor revision of the L5X schema.</value>
    /// <remarks>This is always 1.0. If the R</remarks>
    public Revision? SchemaRevision
    {
        get => Attribute(L5XName.SchemaRevision)?.Value.Parse<Revision>();
        set => SetAttributeValue(L5XName.SchemaRevision, value);
    }

    /// <summary>
    /// Gets the value of the software revision for the current L5X content.
    /// </summary>
    /// <value>A <see cref="Revision"/> type that represent the major/minor revision of the software.</value>
    public Revision? SoftwareRevision
    {
        get => Attribute(L5XName.SoftwareRevision)?.Value.Parse<Revision>();
        set => SetAttributeValue(L5XName.SoftwareRevision, value);
    }

    /// <summary>
    /// Gets the name of the Logix component that is the target of the current L5X context.
    /// </summary>
    public string? TargetName
    {
        get => Attribute(L5XName.TargetName)?.Value;
        set => SetAttributeValue(L5XName.TargetName, value);
    }

    /// <summary>
    /// Gets the type of Logix component that is the target of the current L5X context.
    /// </summary>
    public string? TargetType
    {
        get => Attribute(L5XName.TargetType)?.Value;
        set => SetAttributeValue(L5XName.TargetType, value);
    }

    /// <summary>
    /// Gets the value indicating whether the current L5X is contextual..
    /// </summary>
    public bool? ContainsContext
    {
        get => Attribute(L5XName.ContainsContext)?.Value.Parse<bool>();
        set => SetAttributeValue(L5XName.ContainsContext, value);
    }

    /// <summary>
    /// Gets the owner that exported the current L5X file.
    /// </summary>
    public string? Owner
    {
        get => Attribute(L5XName.Owner)?.Value;
        set => SetAttributeValue(L5XName.Owner, value);
    }

    /// <summary>
    /// Gets the date time that the L5X file was exported.
    /// </summary>
    public DateTime? ExportDate => Attribute(L5XName.ExportDate)?.Value is not null
        ? DateTime.ParseExact(Attribute(L5XName.ExportDate)?.Value, DateTimeFormat, null)
        : default;

    /// <summary>
    /// Gets the root controller element of the L5X file. 
    /// </summary>
    public XElement GetController() => Element(L5XName.Controller) ?? throw new L5XException(L5XName.Controller, this);

    /// <summary>
    /// Gets a top level container element from the root controller element of the L5X.
    /// </summary>
    /// <param name="name">The name of the container to retrieve.</param>
    /// <returns>A <see cref="XElement"/> representing the container with the provided name.</returns>
    /// <exception cref="L5XException">The element does not exist.</exception>
    public XElement GetContainer(string name) => GetController().Element(name) ?? throw new L5XException(name, this);

    /// <summary>
    /// Merges all top level containers and their immediate child elements between the current L5X content and the
    /// provided L5X content. Will overwrite if specified.
    /// </summary>
    /// <param name="l5X">The L5X element to merge with the current target element.</param>
    /// <param name="overwrite">A flag to indicate whether to overwrite child elements of matching name.</param>
    internal void Merge(L5X l5X, bool overwrite)
    {
        var containerPairs = GetContainers()
            .Join(l5X.GetContainers(), e => e.Name, e => e.Name, (a, b) => new { a, b })
            .ToList();

        foreach (var pair in containerPairs)
            MergeContainers(pair.a, pair.b, overwrite);
    }

    private static void Normalize(XContainer content)
    {
        var controller = new XElement(L5XName.Controller, new XAttribute(L5XName.Use, Use.Context));

        foreach (var container in Containers)
        {
            var existing = content.Descendants(container).FirstOrDefault();
            
            if (existing is not null)
            {
                controller.Add(existing);
                continue;
            }

            controller.Add(new XElement(container));
        }

        var current = content.Element(L5XName.Controller);

        if (current is null)
        {
            content.Add(controller);
            return;
        }

        current.ReplaceWith(controller);
    }

    private static void MergeContainers(XContainer target, XContainer source, bool overwrite)
    {
        foreach (var element in source.Elements())
        {
            var match = target.Elements().FirstOrDefault(e => e.LogixName() == element.LogixName());

            if (match is null)
            {
                target.Add(element);
                continue;
            }

            if (overwrite)
                match.ReplaceWith(element);
        }
    }

    /// <summary>
    /// Gets all primary/top level L5X component containers in the current L5X file.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="XElement"/> representing the L5X component containers.</returns>
    private IEnumerable<XElement> GetContainers() => Containers.Select(name => GetController().Element(name)).ToList();
}