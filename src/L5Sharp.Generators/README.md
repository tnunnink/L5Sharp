# L5Sharp.Generators
A collection of Roslyn incremental source generators for the L5Sharp library.

## Overview
`L5Sharp.Generators` provides source generators that perform automatic type registration with the static `LogixSerializer` 
and `LogixType` classes. This allows the core library to avoid using reflection or manual registration code which allow these
types to be deserialized from L5X content. 

This library is primarily used by `L5Sharp.Core`. Developers should not need to reference this project
unless they intend to create custom types that implement `LogixElement` or `LogixData`. If you want to auto generic custom
UDT types from a given L5X, use `L5Sharp.Generators.Data`, which will build classes for all DataType and AddOnInstructions
found in the file.

## Generators

### LogixSerializerRegistrationGenerator
Automatically discovers and registers types decorated with the `[LogixElement]` attribute. This allows L5Sharp to
register these types in the `LogixSerializer` class without manual configuration or use of reflection.

### LogixTypeRegistrationGenerator
Automatically discovers and registers classes decorated with the `[LogixData]` attribute. This ensures that L5Sharp
knows how to instantiate predefined or custom `LogixData` types to populate tag data in memory.

### ModuleInitializerGenerator
A utility generator that adds the `[ModuleInitializer]` attribute polyfill for libraries targeting frameworks below .NET 5,
enabling the use of module initializers to call the registration methods as soon as the assembly is loaded.

## Installation
Add the `L5Sharp.Generators` package to your project as an analyzer/development dependency.

```xml

<ItemGroup>
    <PackageReference Include="L5Sharp.Generators" Version="x.x.x" OutputItemType="Analyzer"
                      ReferenceOutputAssembly="false"/>
    <PackageReference Include="L5Sharp.Core" Version="x.x.x"/>
</ItemGroup>
```

## Usage

For `LogixTypeRegistrationGenerator`, decorate your custom classes with the `LogixData` attribute:

```csharp
[LogixData("MyCustomType")]
public class MyCustomType : StructureData
{
    // ...
}
```

The generator will automatically produce the registration code:

```csharp
LogixType.Register<MyCustomType>("MyCustomType");
```

## Feedback

If you encounter any issues or have suggestions, please open an issue on
the [GitHub repository](https://github.com/tnunnink/L5Sharp/issues).
