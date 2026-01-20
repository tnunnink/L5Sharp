# L5Sharp.Generators.Data

A Roslyn incremental source generator that automatically creates strongly-typed L5Sharp `LogixData` classes from L5X files.

## Overview

L5Sharp.Generators.Data simplifies the process of working with custom User-Defined Types (UDTs) and 
Add-On Instructions (AOIs) in your C# projects. Instead of manually defining classes to match your Logix data structures,
you can include your L5X project file as an `AdditionalFiles` item in your project. The generator will then produce the 
corresponding C# classes, complete with automatic registration into the L5Sharp type system.

## Features

- **Automatic Type Generation**: Generates C# classes for all UDTs and AOIs found in the provided L5X files.
- **Incremental Generation**: Optimized for performance using the Roslyn Incremental Generator API.
- **Automatic Registration**: Generates a `ModuleInitializer` to register all produced types with `LogixType.Register`, 
ensuring they are available to L5Sharp at runtime.
- **Type Safety**: Maps Logix members to their corresponding `LogixData` or atomic types in C#.
- **Seamless Integration**: Works directly with the `L5Sharp.Core` library.

## Installation

Add the `L5Sharp.Generators.Data` package to your project. Since this is a source generator, 
it should be added as an analyzer/development dependency.

```xml
<ItemGroup>
  <PackageReference Include="L5Sharp.Generators.Data" Version="x.x.x" OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
  <PackageReference Include="L5Sharp.Core" Version="x.x.x" />
</ItemGroup>
```

## Usage

1. **Add L5X Files**: Add the L5X file containing your type definitions to your project as `AdditionalFiles`.

```xml
<ItemGroup>
  <AdditionalFiles Include="MyLogixProject.L5X" />
</ItemGroup>
```

2. **Access Generated Types**: Once added, the generator will produce classes for each UDT/AOI. You can then use these types just like any other L5Sharp type.

```csharp
// If you have a UDT named 'MyCustomType' in your L5X.
var myTag = Tag.New<MyCustomType>("SomeTag");
myTag.Value.As<MyCustomType>().SomeMember = 123;
```

3. **Automatic Registration**: You don't need to manually call `LogixType.Register`. 
The generator creates a `LogixDataRegistration` class with a `[ModuleInitializer]` that handles this for you when 
the assembly is loaded.

## How it Works

The generator scans all `AdditionalFiles` with an `.L5X` extension. It parses the `DataTypes` and `AddOnInstructionDefinitions`
sections to build a model of the types. For each type, it generates a partial class inheriting from `StructureData` 
or `StringData` (depending on the type family), as well as properties for all type members in the definition.

## Feedback
If you encounter any issues or have suggestions, please open an issue on the [GitHub repository](https://github.com/tnunnink/L5Sharp/issues).
