using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Enums;

namespace L5Sharp;

/// <summary>
/// A <see cref="XElement"/> decorator that adds members for interacting with the root L5X content. This class should
/// represent the root RSLogix5000Content element of the L5X file.
/// </summary>
/// <remarks>
/// Most of these methods and properties are meant to be internal and are used by <see cref="LogixContent"/>.
/// However, the user can use this type if they need direct access to the underlying XML to perform custom query
/// or extend the API.
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
    private static readonly List<string> Containers = new()
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
        // files so that we won't have issues getting top level containers. When saving we can remove unused containers.
        Normalize();
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
    public XElement GetController() => Element(L5XName.Controller) ?? throw this.L5XError(L5XName.Controller);

    /// <summary>
    /// Gets a top level container element from the root controller element of the L5X.
    /// </summary>
    /// <param name="name">The name of the container to retrieve.</param>
    /// <returns>A <see cref="XElement"/> representing the container with the provided name.</returns>
    /// <exception cref="InvalidOperationException">The element does not exist.</exception>
    public XElement GetContainer(string name) => GetController().Element(name) ?? throw this.L5XError(name);

    /// <summary>
    /// Merges all top level containers and their immediate child elements between the current L5X content and the
    /// provided L5X content. Will overwrite if specified.
    /// </summary>
    /// <param name="l5X">The L5X element to merge with the current target element.</param>
    /// <param name="overwrite">A flag to indicate whether to overwrite child elements of matching name.</param>
    internal void MergeContent(L5X l5X, bool overwrite)
    {
        var containerPairs = GetContainers()
            .Join(l5X.GetContainers(), e => e.Name, e => e.Name, (a, b) => new { a, b })
            .ToList();

        foreach (var pair in containerPairs)
            MergeContainers(pair.a, pair.b, overwrite);
    }

    /// <summary>
    /// Create document, adds default declaration, and saves the current L5X content to the specified file name.
    /// </summary>
    /// <param name="fileName">A string that contains the name of the file.</param>
    internal void SaveContent(string fileName)
    {
        //This will sanitize containers that were perhaps added when normalizing that went unused.
        foreach (var container in GetContainers().Where(c => !c.HasElements))
            container.Remove();

        var declaration = new XDeclaration("1.0", "UTF-8", "yes");
        var document = new XDocument(declaration);
        document.Add(this);
        document.Save(fileName);
    }

    /// <summary>
    /// If no root controller element exists, adds new context controller and moves all root elements into that controller
    /// element. Then adds missing top level containers to ensure consistent structure of the root L5X.
    /// </summary>
    private void Normalize()
    {
        if (Element(L5XName.Controller) is null)
        {
            var context = new XElement(L5XName.Controller, new XAttribute(L5XName.Use, Use.Context));
            context.Add(Elements());
            RemoveNodes();
            Add(context);
        }

        var controller = Element(L5XName.Controller)!;

        foreach (var container in from container in Containers
                 let existing = controller.Element(container)
                 where existing is null
                 select container)
        {
            controller.Add(new XElement(container));
        }
    }

    /// <summary>
    /// Given to top level containers, adds or replaces all child elements matching based on the logix name of the elements.
    /// </summary>
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