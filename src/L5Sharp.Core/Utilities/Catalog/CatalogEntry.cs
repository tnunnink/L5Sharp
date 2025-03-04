﻿using System.Collections.Generic;

namespace L5Sharp.Core;

/// <summary>
/// A set of properties that defines a catalog entry for a <see cref="Module"/> component.
/// </summary>
/// <remarks>
/// This object must be retrieved from the <see cref="ModuleCatalog"/> service object. It serves as a container
/// of data that can be retrieved in order to create a valid <see cref="Module"/> instance using a known
/// <see cref="CatalogNumber"/>. 
/// </remarks>
public class CatalogEntry
{
    /// <summary>
    /// Gets the <see cref="CatalogNumber"/> of the current <see cref="CatalogEntry"/>.
    /// </summary>
    public string CatalogNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets the description of the <see cref="CatalogEntry"/>.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets the <see cref="Vendor"/> of the current <see cref="CatalogEntry"/>.
    /// </summary>
    public Vendor Vendor { get; set; } = Vendor.Unknown;

    /// <summary>
    /// Gets the <see cref="ProductType"/> of the current <see cref="CatalogEntry"/>.
    /// </summary>
    public ProductType ProductType { get; set; } = ProductType.Unknown;

    /// <summary>
    /// Gets the <see cref="ushort"/> code number that identifies the <see cref="CatalogEntry"/>. 
    /// </summary>
    public ushort ProductCode { get; set; }

    /// <summary>
    /// Gets a collection of valid <see cref="Revisions"/> for the <see cref="CatalogEntry"/>.
    /// </summary>
    public IEnumerable<Revision> Revisions { get; set; } = [];

    /// <summary>
    /// Gets a collection of <see cref="ModuleCategory"/> that apply to the module.
    /// </summary>
    public IEnumerable<string> Categories { get; set; } = [];

    /// <summary>
    /// Gets the set of <see cref="Ports"/> that define the connections of the module.
    /// </summary>
    public IEnumerable<PortInfo> Ports { get; set; } = [];
}