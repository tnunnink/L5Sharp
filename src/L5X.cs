using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Serialization;
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
    private const string DateTimeFormat = "ddd MMM d HH:mm:ss yyyy";

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
    /// Gets either the controller root container element if it exists, or just the current RSLogixContent element.
    /// One of these two will represent the root of the container elements depending on the type of L5X export file.
    /// </summary>
    private XContainer Root => Element(L5XName.Controller) is not null ? Element(L5XName.Controller)! : this;

    /// <summary>
    /// Gets the known container element for the specified component name.
    /// </summary>
    /// <param name="name">The name of the component.</param>
    /// <returns>The <see cref="XContainer"/> which representing the root for the component name.</returns>
    /// <exception cref="ArgumentException"><c>name</c> does not have a known container.</exception>
    public XElement? GetContainer(XName name)
    {
        if (!Containers.TryGetValue(name.ToString(), out var container))
            throw new ArgumentException(
                $"The provided name {name} does not have a corresponding component container.");

        return Root.Elements(container).FirstOrDefault();
    }

    /// <summary>
    /// Gets all primary/top level L5X component containers in the current L5X file.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="XElement"/> representing the L5X component containers.</returns>
    public IEnumerable<XElement> GetContainers() => Containers.Values
        .Select(name => Root.Elements(name).FirstOrDefault())
        .Where(e => e is not null)
        .ToList();

    /// <summary>
    /// Creates a new <see cref="XElement"/> representing the root logix content containing the provided component.
    /// </summary>
    /// <param name="target">The target component of the L5X.</param>
    /// <returns>A <see cref="XElement"/> representing the new L5X content element with default properties and target
    /// attributes set to specified component name and type.</returns>
    internal static XElement Create<TComponent>(TComponent target) where TComponent : ILogixComponent
    {
        var content = new XElement(L5XName.RSLogix5000Content);
        content.Add(new XAttribute(L5XName.SchemaRevision, new Revision().ToString()));
        content.Add(new XAttribute(L5XName.TargetName, target.Name));
        content.Add(new XAttribute(L5XName.TargetType, target.GetType().GetLogixName()));
        content.Add(new XAttribute(L5XName.ContainsContext, target is not Controller));
        content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
        content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateTimeFormat)));

        var serializer = LogixSerializer.GetSerializer<TComponent>();
        var component = serializer.Serialize(target);
        component.AddFirst(new XAttribute(L5XName.Use, Use.Target));

        var controller = component.Name == L5XName.Controller ? component : CreateContext();
        EnsureContainersAdded(controller);

        if (component.Name != L5XName.Controller)
        {
            if (!Containers.TryGetValue(component.Name.ToString(), out var containerName))
                throw new ArgumentException(
                    $"The provided name {component.Name} does not have a corresponding component container.");

            controller.Element(containerName)?.Add(component);
        }

        content.Add(controller);
        return content;
    }

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

    /// <summary>
    /// Modifies the existing root content element to ensure it contains a controller element with all
    /// known component containers (i.e. DataTypes, Modules, Tag. etc).
    /// </summary>
    /// <remarks>
    /// This ensures that adding new content will be successful.
    /// This will maintain existing content by transferring it under the controller element
    /// if one is generated by calling this method.
    /// </remarks>
    /// <exception cref="InvalidOperationException">The current root content is not a component container.</exception>
    internal void Normalize()
    {
        var content = Elements().FirstOrDefault();

        if (content is null)
        {
            EnsureContextAdded();
            return;
        }

        //if the root content is controller, then just ensure all containers are added if not.
        if (content.Name == L5XName.Controller)
        {
            EnsureContainersAdded(content);
            return;
        }

        if (!Containers.ContainsValue(content.Name.ToString()))
            throw new InvalidOperationException(
                $"The root content element {content.Name} is not an valid component container.");

        //Get a new controller element as the context.
        var controller = CreateContext();

        //Add containers and preserve current content. 
        NormalizeContainers(controller, content);

        //Replace the current root content with the new normalized controller context.
        content.ReplaceWith(controller);
    }

    private void EnsureContextAdded()
    {
        var controller = CreateContext();
        EnsureContainersAdded(controller);
        Add(controller);
    }

    private static void NormalizeContainers(XContainer controller, XElement content)
    {
        foreach (var entry in Containers)
        {
            //Add the existing content in place of an empty container.
            if (entry.Value == content.Name)
            {
                controller.Add(content);
                continue;
            }

            controller.Add(new XElement(entry.Value));
        }
    }

    private static void EnsureContainersAdded(XContainer content)
    {
        foreach (var entry in Containers.Where(entry => content.Element(entry.Value) is null))
            content.Add(new XElement(entry.Value));
    }

    private static XElement CreateContext(string? name = null)
    {
        var element = new XElement(L5XName.Controller);

        element.Add(new XAttribute(L5XName.Use, Use.Context));

        if (!string.IsNullOrEmpty(name))
            element.Add(new XAttribute(L5XName.Use, Use.Context));

        return element;
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
}