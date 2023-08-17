using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Elements;
using L5Sharp.Extensions.Catalog;

namespace L5Sharp.Extensions;

/// <summary>
/// A class containing extensions for the <see cref="Tag"/> component.
/// </summary>
public static class ModuleExtensions
{
    /// <summary>
    /// Gets the slot number of the current module if one exists. If the module does not have an slot, returns null.
    /// </summary>
    /// <value>An <see cref="byte"/> representing the slot number of the module.</value>
    /// <remarks>
    /// This is a helper that just looks for an upstream <see cref="Port"/> with a valid slot byte number.
    /// </remarks>
    public static byte? Slot(this Module module) =>
        module.Ports.FirstOrDefault(p => p.Upstream && p.Address.IsSlot)?.Address.ToSlot();

    /// <summary>
    /// Gets the IP address of the current module if one exists. If the module does not have an IP, returns null.
    /// </summary>
    /// <value>An <see cref="IPAddress"/> representing the IP of the module.</value>
    /// <remarks>
    /// This is a helper property that just looks for <see cref="Port"/> for an Ethernet port with a
    /// valid IP address.
    /// </remarks>
    public static IPAddress? IP(this Module module) => module.Ports
        .FirstOrDefault(p => p is { Type: "Ethernet", Address.IsIPv4: true })?.Address
        .ToIPAddress();

    /// <summary>
    /// Gets the <c>Local</c> module or module that represents the controller of the module collection.
    /// </summary>
    /// <param name="modules">A collection of modules.</param>
    /// <returns>A single <see cref="Module"/> which is named local if found; Otherwise, <c>null</c>.</returns>
    /// <remarks>This is a helper to concisely get the controller or root local module from the modules collection.</remarks>
    public static Module? Local(this IEnumerable<Module> modules) => modules.SingleOrDefault(m => m.Name == "Local");

    /// <summary>
    /// Gets the parent module of this module component defined in the current L5X content.
    /// </summary>
    /// <returns>A <see cref="Module"/> representing the parent of this module if it exists; otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// The L5X structure serializes modules in a flat list with each element having properties (ParentModule, ParentModPortId)
    /// defining the parent/child IO tree relationship. It would be nice to navigate this hierarchy programatically,
    /// hence the reason for this extension method. Of course, this requires the module is attached to the L5X content.
    /// In-memory created modules will inherently return an empty collection, as there is no L5X structure to introspect.
    /// </remarks>
    public static Module? Parent(this Module module)
    {
        var parent = module.Serialize().Parent?.Elements().FirstOrDefault(m => m.LogixName() == module.ParentModule);
        return parent is not null ? new Module(parent) : default;
    }

    /// <summary>
    /// Gets the child modules of this module component defined in the current L5X content. 
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="Module"/> components that have the <c>ParentModule</c> property
    /// configured as the name of this module.
    /// </returns>
    /// <remarks>
    /// The L5X structure serializes modules in a flat list with each element having properties (ParentModule, ParentModPortId)
    /// defining the parent/child IO tree relationship. It would be nice to navigate this hierarchy programatically,
    /// hence the reason for this extension method. Of course, this requires the module is attached to the L5X content.
    /// In-memory created modules will inherently return an empty collection, as there is no L5X structure to introspect.
    /// </remarks>
    public static IEnumerable<Module> Modules(this Module module)
    {
        return module.Serialize().Parent?.Elements()
                   .Where(m => m.Attribute(L5XName.ParentModule)?.Value == module.Name)
                   .Select(e => new Module(e))
               ?? Enumerable.Empty<Module>();
    }

    /// <summary>
    /// Returns a collection of all non-null <see cref="Tag"/> objects for the current Module, including all
    /// config, input, and output tags.
    /// </summary>
    /// <value>An <see cref="IEnumerable{T}"/> containing the base tags for the Module.</value>
    /// <remarks>
    /// Since module tags are nested within different layers of complex types, it can be difficult to just
    /// get a single list of all module tags. This extension makes that easy by sifting through the object and returning
    /// a flat list containing all non-null config, input, and output tags defined for the <see cref="Module"/> component.
    /// </remarks>
    public static IEnumerable<Tag> Tags(this Module module)
    {
        var tags = new List<Tag>();

        if (module.Communications is null) return tags;

        if (module.Communications.ConfigTag is not null)
            tags.Add(module.Communications.ConfigTag);

        foreach (var connection in module.Communications.Connections)
        {
            if (connection.InputTag is not null)
                tags.Add(connection.InputTag);

            if (connection.OutputTag is not null)
                tags.Add(connection.OutputTag);
        }

        return tags;
    }

    /// <summary>
    /// Returns the module's config tag object contained in the communications element.
    /// </summary>
    /// <param name="module">The current <see cref="Module"/> component.</param>
    /// <returns>A <see cref="Tag"/> containing the module's config tag data.</returns>
    /// <remarks>This is a simple helper to make accessing the module config data more concise.</remarks>
    public static Tag? Config(this Module module) => module.Communications?.ConfigTag;

    /// <summary>
    /// Adds a child module to the current module object by updating the parent module properties, configuring the
    /// child module upstream port, and adding the component to the underlying L5X content.
    /// </summary>
    /// <param name="parent">The module for which to add a child.</param>
    /// <param name="child">The module to add.</param>
    /// <param name="address">The optional <see cref="Address"/> (slot or IP) of the module to add.</param>
    /// <exception cref="InvalidOperationException"><c>parent</c> does not have a downstream port for which to connect
    /// child modules.</exception>
    /// <remarks>
    /// This extension gives us an easy way to add modules hierarchically to the underlying L5X content.
    /// If the parent module is attached to a L5X content file, this will add child module. Otherwise, this method only
    /// 
    /// </remarks>
    public static void Add(this Module parent, Module child, Address? address = default)
    {
        var parentPort = parent.Ports.FirstOrDefault(p => p.Upstream is false);

        if (parentPort is null)
            throw new InvalidOperationException(
                $"The module '{parent.Name}' does not have a port for downstream module connections.");

        if (parentPort.Type == "Ethernet" && address is null) address = Address.DefaultIP();
        if (parentPort.Address.IsSlot && parent.IsAttached && address is null) address = parent.NextSlot();
        address ??= Address.DefaultSlot();

        var childPort = new Port { Id = 1, Type = parentPort.Type, Address = address, Upstream = true };

        child.ParentModule = parent.Name;
        child.ParentModPortId = parentPort.Id;
        child.Ports.Add(childPort);

        var container = parent.Serialize().Parent;

        if (container is null)
            throw new InvalidOperationException($"The module '{parent.Name}' is not attached to and L5X content file.");

        container.Add(child.Serialize());
    }

    /// <summary>
    /// Gets the next largest slot number for the current module by introspecting the slot numbers of all other
    /// child modules of this parent module. 
    /// </summary>
    /// <param name="module">The current <see cref="Module"/> component.</param>
    /// <returns>
    /// A <see cref="Address"/> containing the next highest slot number in the rack/chassis.
    /// </returns>
    public static Address NextSlot(this Module module)
    {
        var children = module.Serialize().Parent?.Elements()
            .Where(m => m.Attribute(L5XName.ParentModule)?.Value == module.Name);

        var next = children?.Select(c => c.Descendants(L5XName.Port)
                .FirstOrDefault(p => p.Attribute(L5XName.Upstream)?.Value.Parse<bool>() is true &&
                                     byte.TryParse(p.Attribute(L5XName.Address)?.Value, out _))
                ?.Attribute(L5XName.Address)?.Value.Parse<byte>())
            .OrderByDescending(b => b)
            .FirstOrDefault();

        return next.HasValue ? Address.Slot(next.Value) : Address.DefaultSlot();
    }
    
    /// <summary>
    /// Creates a new <see cref="Components.Module"/> with the provided name and catalog number.
    /// </summary>
    /// <param name="module"></param>
    /// <param name="name">The name of the module</param>
    /// <param name="catalogNumber">The catalog number to lookup a catalog entry for.</param>
    /// <param name="address"></param>
    /// <returns>A new <see cref="Components.Module"/> object initialized with data return by the catalog service.</returns>
    /// <exception cref="InvalidOperationException">The module catalog service could not load the installed catalog
    /// database file -or- catalog number does not exist in the catalog database.</exception>
    /// <exception cref="ArgumentException"><c>catalogNumber</c> is null or empty.</exception>
    /// <remarks>This factory method uses the <see cref="ModuleCatalog"/> service to lookup info for the specified
    /// catalog number. If RSLogix is not installed on the current environment, this will throw an exception.</remarks>
    public static Module Add(this Module module, string name, string catalogNumber, Address? address = null)
    {
        var catalog = new ModuleCatalog();
        var entry = catalog.Lookup(catalogNumber);

        return new Module
        {
            Name = name,
            CatalogNumber = entry.CatalogNumber,
            Revision = entry.Revisions.Max(),
            Vendor = entry.Vendor,
            ProductType = entry.ProductType,
            ProductCode = entry.ProductCode,
            Ports = new LogixContainer<Port>(
                entry.Ports.Select(p => new Port
                {
                    Id = p.Number,
                    Type = p.Type,
                    Address = p.Type == "Ethernet" ? Address.DefaultIP() : Address.DefaultSlot(),
                    Upstream = !p.DownstreamOnly
                }).ToList()),
            Description = entry.Description
        };
    }
}