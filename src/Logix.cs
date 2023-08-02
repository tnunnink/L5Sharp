using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using L5Sharp.Catalog;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Elements;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp;

/// <summary>
/// A global static class containing methods for creating types, components, and other useful helpers.
/// </summary>
public static class Logix
{
    /// <summary>
    /// Creates a new <see cref="Components.Tag"/> with the provided name and specified type parameter.
    /// </summary>
    /// <param name="name">The name of the tag.</param>
    /// <typeparam name="TLogixType">The logix data type of the tag. Type must have parameterless constructor to create.</typeparam>
    /// <returns>A new <see cref="Tag"/> object with specified parameters.</returns>
    public static Tag Tag<TLogixType>(string name) where TLogixType : LogixType, new() =>
        new() { Name = name, Value = new TLogixType() };

    /// <summary>
    /// Creates a new <see cref="Task"/> component with the provide properties.
    /// </summary>
    /// <param name="name">The name of the task.</param>
    /// <param name="type">The <see cref="Enums.TaskType"/> specifying the type of task.</param>
    /// <param name="description">The description of the task.</param>
    /// <returns>A <see cref="Components.Task"/> component with the provided property values.</returns>
    public static Task Task(string? name = null, TaskType? type = null, string? description = null) => new()
    {
        Name = name ?? string.Empty,
        Type = type ?? TaskType.Periodic,
        Description = description ?? string.Empty
    };

    /// <summary>
    /// Creates a new <see cref="Components.Module"/> with the provided name and catalog number.
    /// </summary>
    /// <param name="name">The name of the module</param>
    /// <param name="catalogNumber">The catalog number to lookup a catalog entry for.</param>
    /// <param name="description">The option description of the module.</param>
    /// <returns>A new <see cref="Components.Module"/> object initialized with data return by the catalog service.</returns>
    /// <exception cref="InvalidOperationException">The module catalog service could not load the installed catalog
    /// database file -or- catalog number does not exist in the catalog database.</exception>
    /// <exception cref="ArgumentException"><c>catalogNumber</c> is null or empty.</exception>
    /// <remarks>This factory method uses the <see cref="ModuleCatalog"/> service to lookup info for the specified
    /// catalog number. If RSLogix is not installed on the current environment, this will throw an exception.</remarks>
    public static Module Module(string name, string catalogNumber, string? description = null)
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
            Description = description ?? string.Empty
        };
    }
}