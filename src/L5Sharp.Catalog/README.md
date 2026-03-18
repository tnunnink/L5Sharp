# L5Sharp.Catalog

A .NET library for managing and querying Rockwell Automation module definitions, providing a robust infrastructure for discovering hardware configurations and creating `Module` instances for [L5Sharp](https://github.com/tnunnink/L5Sharp).

## Overview

`L5Sharp.Catalog` decouples the core Logix model from the underlying hardware database. It provides a searchable repository of module definitions (catalog numbers, revisions, communication details, and connection templates) that can be seeded from multiple sources, including L5X project files, Rockwell's internal Catalog Services database, or custom definitions.

By using `L5Sharp.Catalog`, developers can easily discover valid module configurations and instantiate `Module` objects with all necessary attributes and child elements pre-configured, mirroring how they would be created within RSLogix/Studio 5000.

## Features

- **Unified Module Catalog**: A single interface (`IModuleCatalog`) to query and retrieve module definitions by catalog number and revision.
- **Fluent Builder API**: Easily configure and seed your catalog using `ModuleCatalogBuilder`.
- **Multiple Data Sources**:
    - **Embedded Defaults**: Includes a built-in library of common Rockwell Automation modules for immediate use.
    - **L5X Integration**: Extract module definitions directly from existing L5X project files.
    - **Rockwell Database Support**: Read directly from the local Rockwell Automation Catalog Services database.
- **Strongly-Typed Revisions**: Manage multiple versions of the same hardware with semantic revision support.
- **Module Factory Methods**: Create fully initialized `Module` instances directly from the catalog.

## Installation

Install `L5Sharp.Catalog` via NuGet:

```powershell
Install-Package L5Sharp.Catalog
```

## Quick Start

### Building a Catalog

Use the `ModuleCatalogBuilder` to aggregate definitions from various sources:

```csharp
var catalog = new ModuleCatalogBuilder()
    .WithDefaultModules()                               // Load built-in common modules
    .WithModulesFromL5X("StandardTemplates.L5X")        // Seed from a project file
    .WithModulesFromRAD()                               // Load from local Rockwell database
    .Build();
```

### Creating Modules

Once built, use the catalog to create new `Module` instances for your L5X project:

```csharp
// Create the latest revision of a ControlLogix processor
var controller = catalog.Create("MainController", "1756-L83E");

// Create a specific revision of an I/O module with custom configuration
var inputModule = catalog.Create("Input_Slot1", "1756-IB16", new Revision(2, 1), m =>
{
    m.Description = "Main Intake Sensors";
    m.ParentModule = "Local";
    m.ParentPortId = 1;
    m.Slot = 1;
});
```

### Querying Definitions

You can also browse the catalog to find available hardware:

```csharp
// Get all definitions for a specific catalog number
var revisions = catalog.Definitions("1756-IB16");

// Find a specific definition
if (catalog.TryGetDefinition("1756-EN2T", out var definition))
{
    Console.WriteLine($"Found {definition.CatalogNumber} with {definition.Ports.Count()} ports.");
}
```

## Requirements

- .NET Standard 2.0 or .NET 8.0+
- [L5Sharp.Core](https://github.com/tnunnink/L5Sharp)

## License

This project is licensed under the MIT License.
