using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp;

/// <summary>
/// A <see cref="XElement"/> decorator that adds members for interacting with the root L5X content.
/// </summary>
/// <remarks>
/// Most of these methods and properties are meat to be internal and are referenced when mutating or querying the content
/// of an L5X file.
/// </remarks>
public class L5X : XElement
{
    /// <summary>
    /// The date/time format for the L5X content.
    /// </summary>
    public const string DateTimeFormat = "ddd MMM d HH:mm:ss yyyy";

    private static readonly Dictionary<string, string> Containers = new()
    {
        { L5XName.DataType, L5XName.DataTypes },
        { L5XName.Module, L5XName.Modules },
        { L5XName.AddOnInstructionDefinition, L5XName.AddOnInstructionDefinitions },
        { L5XName.Tag, L5XName.Tags },
        { L5XName.Program, L5XName.Programs },
        { L5XName.Task, L5XName.Tasks },
        { L5XName.ParameterConnection, L5XName.ParameterConnections },
        { L5XName.Trend, L5XName.Trends },
        { L5XName.QuickWatchList, L5XName.QuickWatchLists }
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
    public Revision? SchemaRevision => this.TryGetValue<Revision>(L5XName.SchemaRevision);

    /// <summary>
    /// Gets the value of the software revision for the current L5X content.
    /// </summary>
    /// <value>A <see cref="Revision"/> type that represent the major/minor revision of the software.</value>
    public Revision? SoftwareRevision => this.TryGetValue<Revision>(L5XName.SoftwareRevision);

    /// <summary>
    /// Gets the name of the Logix component that is the target of the current L5X context.
    /// </summary>
    public string? TargetName => this.TryGetValue<string>(L5XName.TargetName);

    /// <summary>
    /// Gets the type of Logix component that is the target of the current L5X context.
    /// </summary>
    public string? TargetType => this.TryGetValue<string>(L5XName.TargetType);

    /// <summary>
    /// Gets the value indicating whether the current L5X is contextual..
    /// </summary>
    public bool? ContainsContext => this.TryGetValue<bool>(L5XName.ContainsContext);

    /// <summary>
    /// Gets the owner that exported the current L5X file.
    /// </summary>
    public string? Owner => this.TryGetValue<string>(L5XName.Owner);

    /// <summary>
    /// Gets the date time that the L5X file was exported.
    /// </summary>
    public DateTime? ExportDate => this.TryGetDateTime(L5XName.ExportDate, DateTimeFormat);

    /// <summary>
    /// Gets the root controller element of the L5X file. 
    /// </summary>
    public XElement Controller =>
        Element(L5XName.Controller)
        ?? throw new InvalidOperationException(
            "Could not retrieve the root controller element XML content. Verify valid L5X format.");

    /// <summary>
    /// Gets the known container element for the specified component type name.
    /// </summary>
    /// <param name="name">The name of the component.</param>
    /// <returns>The <see cref="XContainer"/> which representing the root for the component name.</returns>
    /// <exception cref="ArgumentException"><c>name</c> does not have a known container.</exception>
    public XElement GetContainer(XName name)
    {
        var container = DetermineContainer(name);

        return Controller.Element(container) ??
               throw new InvalidOperationException(
                   $"The container with name {container} does not exist in the current L5X.");
    }

    /// <summary>
    /// Gets the known container element for the specified component type name.
    /// </summary>
    /// <param name="name">The name of the component.</param>
    /// <returns>The <see cref="XContainer"/> which representing the root for the component name.</returns>
    /// <exception cref="ArgumentException"><c>name</c> does not have a known container.</exception>
    public IEnumerable<XElement> GetContainers(XName name)
    {
        var container = DetermineContainer(name);

        return Controller.Descendants(container);
    }

    /// <summary>
    /// Gets all primary/top level L5X component containers in the current L5X file.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="XElement"/> representing the L5X component containers.</returns>
    public IEnumerable<XElement> GetContainers() => Containers.Values.Select(name => Controller.Element(name)).ToList();

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

        foreach (var container in Containers.Values)
        {
            var existing = content.Descendants(container).FirstOrDefault();

            //Add the existing content in place of an empty container.
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

    private static string DetermineContainer(XName name)
    {
        var target = name.ToString();

        if (Containers.TryGetValue(target, out var container))
            return container;

        if (Containers.ContainsValue(target))
            return target;

        throw new ArgumentException($"The provided name {name} does not have a corresponding component container.");
    }

    private static void MergeContainers(XContainer target, XContainer source, bool overwrite)
    {
        foreach (var element in source.Elements())
        {
            var name = element.LogixName();
            var match = target.Elements().FirstOrDefault(e => e.LogixName() == name);

            if (match is null)
            {
                target.Add(element);
                continue;
            }

            if (overwrite)
                match.ReplaceWith(element);
        }
    }

    private IEnumerable<XElement> GetScopedContainers(ILogixScoped scoped, string container)
    {
        if (scoped.Scope == Scope.Controller)
        {
            return Descendants(container).Where(c => c.Parent?.Name == L5XName.Controller);
        }

        if (scoped.Scope == Scope.Program)
        {
            return Descendants(container)
                .Where(c => c.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() == scoped.Container);
        }

        if (scoped.Scope == Scope.Instruction)
        {
            return Descendants(container)
                .Where(c => c.Ancestors(L5XName.AddOnInstructionDefinition).FirstOrDefault()?.LogixName() ==
                            scoped.Container);
        }

        return Descendants(container);
    }
}