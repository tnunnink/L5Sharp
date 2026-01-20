# L5Sharp.Core

A robust .NET library for programmatically interacting with Rockwell Automation's L5X import/export files.

## Overview

L5Sharp.Core is the foundation of the L5Sharp ecosystem. It provides a strongly typed, intuitive, and performant API
for reading, querying, modifying, and generating Logix L5X content. Whether you're automating project configuration,
performing deep static analysis, or generating code, L5Sharp.Core offers the tools to handle Logix components and
data structures with ease.

## Key Features

- **High-Performance L5X Parsing**: Load and parse large L5X files efficiently using `L5X.Load` or `L5X.Parse`.
- **Strongly Typed Component Model**: Work with native C# representations of Logix components like `Tag`, `Program`,
  `Routine`, `DataType`, `AddOnInstruction`, `Module`, and more.
- **Advanced Querying with LINQ**: Use the `Query<T>()` API to perform complex searches across your entire project.
- **Intuitive Tag Data System**: Navigate and manipulate complex nested tag structures using the `LogixData` type
  system, which handles member alignment and data types automatically.
- **Fast Lookup Indexing**: Enable project-wide indexing for O(1) lookups of components by name or path.
- **Comprehensive Modification Support**: Add, remove, update, or replace any component within the L5X structure.
- **Source Code Analysis**: Parse and analyze Logix RLL (Relay Ladder Logic) and Structured Text code.
- **Extensible Architecture**: Add support for custom serializers or extend existing component models.

## Installation

Install L5Sharp.Core via NuGet:

```powershell
Install-Package L5Sharp.Core
```

## Quick Start

L5Sharp is centered around loading, querying, and updating L5X content. The primary entry points is the `L5X` class.
The following example demonstrates how easy it is to dynamically find all tags of a specific type in the project
and update a member value in a strongly typed way.

```csharp
var project = L5X.Load("MyController.L5X");

var timers = project.Query<Tag>()
    .Where(t => t.DataType == "TIMER" && t.Dimensions == Dimensions.Empty)
    .Select(t => t.Value.As<TIMER>())
    .Where(t => t.PRE < 1000);

timers.ToList().ForEach(t => t.PRE = 5000);

project.Save("MyController.L5X");
```

## Basic Usage

The following sections will walk through the library in more detail to highlight the primary features L5Sharp offers.

### Loading & Creating an L5X

There are several factory methods for loading or creating an `L5X` instance.
These factory methods mostly mimic underlying `XElement` factory methods.

```csharp
// Use async overload for loading.
var project = await L5X.LoadAsync("MyProject.L5X", CancellationToken.None);

// Parse L5X string in memory.
var project = L5X.Parse("<RSLogix5000Content ... />");

// Create new L5X in memory. This will seed the project with the specified processor.
var project = L5X.New("MyProjectName", "1756-L84E", 36.1);

// Create an empty L5X in memory.
var project = L5X.Empty();
```

### Accessing Components

The `L5X` class provides high-level collections for every major component found in the Studio 5k.
Rockwell PLC developers should be familiar with these types and what they contain.

``` csharp
// Access globally scoped components.
var controllerTags = project.Tags;
var userDefinedTypes = project.DataTypes;
var aois = project.AddOnInstructions;
var modules = project.Modules;
var programs = project.Programs;
var tasks = project.Tasks;

// Access locally scoped components
var programTags = content.Programs.First().Tags;
var routines = content.Programs.First().Routines;
```

These collections return a `LogixContainer<T>` where `T` is the type of the compoent.
This is a custom collection class that implement .NET collection interfaces like `IList<T>` and `ICollection`.
All methods essentially query or update the underlying XML sequence. The API should be very straightforarad, but there
are a few extension methods for the component types above that are worth calling out.

```csharp
// Get a component in the collection with the specified name.
var tag = project.Tags.Get("MyTagname");

// Try to get component in the collection and return the instance if found.
var result = project.Tags.TryGet("MyTagName", out var tag);

// Remove a component by name.
var removed = project.Tags.Remove("MyTagName");
```

### Query API

Instead of using the component collection above, `L5X` offers a simple and more flexible method for querying the content
of the project file. Using the `Query` method allows you to find all elements of a specific type in the file.

```csharp
// Tag query examples
var allTags = project.Query<Tag>();
var allTimerTags = project.Query<Tag>(t => t.DataType == "TIMER");
var tagsInProgram = project.Query<Tag>(t => t.Scope.Container == "SomeProgram");
var ioTags = project.Query<Tag>(t => t.TagName.Base.Contains(":"));

// Rung query examples
var allRungs = project.Query<Rung>();
var rungsWithInstruction = project.Query<Rung>(r => r.Text.Contains("ALARM"));
```

This query API searches across all scopes, making it much easier to find every tag in the project that meets
specific filter criteria. Combined with strongly typed element classes and LINQ, this provides powerful capabilities
for querying L5X files.

### Fast Lookups

Another primary feature of L5Sharp.Core is the ability to perform fast lookups of components or entities within the L5X.
This feature is exposed through a set of `Get` and `TryGet` methods on the `L5X` class.

```csharp
// Gets a component by name.
var component = project.Get<DataType>("MyCustomType");

// Try to get a component by name and output the result if found.
var result = project.TryGet<Module>("LocalModuleName", out var component);

// This API supports controller scoped, program scoped, and nested tag member.
var tag = project.Get<Tag>("Program:SomeProgram.MyTagName.Member[2].Value");

// This API supports getting "code" type elements as well.
var result = project.TryGet<Rung>(12, "SomeProgram", "MainRoutine", out var rung);
```

These all operate in O(1) time. When the first call to one of these methods is made, the L5X is indexed and all
`LogixEntity` types are stored in dictionaries. The internal index subscribes to element changes in the root L5X
element,
so that if any changes are made to the project, the index is invalidated and will reindex upon the next call.

> [!WARNING]
> The indexing code is designed to be as performant as possible, but use caution when calling these functions while
> also making changes in a tight loop, for this will cause reindexing each time. This API is mostly meant for fast
> read-only access to the document.

### Entity Reference

All elements that are indexed get stored by a "reference" value. A `Reference` is a class that contains the route or
path to a specific element in the L5X. This class is designed to resemble a URI type identifer for each element.
All elements that impelement `ILogixEntity` will have a `Reference` property which is determined by its place
in the XML hierarchy.

```csharp
// Create a new in memory program tag.
var component = Tag.New<DINT>("Program:SomeProgram.MyTagName");

// Get the Reference property.
var reference = component.Reference;

// Write the results of the reference
Console.WriteLine(reference.Path); // Output: tag://SomeProgram/MyTagName
Console.WriteLine(reference.Type); // Output: tag
Console.WriteLine(reference.Container); // Output: SomeProgram
Console.WriteLine(reference.Id); // Output: MyTagName
```

References have the following format:

`[Type]://[Container/][Routine/]Id[#Fragment]`

- `Type`: The reference type. This specifies _what_ we are referencing (e.g., tag, datatype, rung, etc.).
- `Container`: The container name (Program/AOI). This is optional and used for scoped elements (tag, routine, rung).
- `Routine`: The routine name. This is optional and only used for code elements (rung, line, sheet).
- `Id`: The identifer value (name/number). This specifies which type in the current scope is referenced.
- `Fragment`: Optional metadata that refers to a subproperty or attribute of the target reference. This is mostly used
  to capture specific instruction text within rung references.

The `Reference` class offer several methods for building a reference value.

```csharp
// Create reference to tag with provided name and scope.
var reference = Reference.To<Tag>("MyTag", Scope.Program("MyProgram")); // tag://MyProgram/MyTag

// Create reference to rung with instruction fragment "XIC(LocalTag)"
var reference = Reference.To("rung://MyProgram/MyRoutine/1#XIC(LocalTag)");

// Reference also has implicit string conversion.
Reference reference = "program://MyProgramName"
```

Since the reference value is used as the key for the internal indexing code, we can also use it to perform lookups
in the L5X.

```csharp
var component = project.Get("tag://MyProgram/MyTagName.SomeMember.Array[9].Element.12");

// Since you know this is a tag, you can cast it as needed. As<T>() is a build in cast method available on all types.
var tag = component.As<Tag>();
```

### Creating Components

All components of L5Sharp.Core are designed to be very POCO like on the surface. For the most part, you can create a
new component with the default constructor and initialize properties as needed.

```csharp
var tag = new Tag { Name = "MyTag", Value = 123};
var dataType = new DataType { Name = "MyCustomType", Description = "This is a custom type." };
var task = new Task { Name = "PeriodicTask", Type = TaskType.Periodic, Rate = 100 };
```

Most components will have constructors that help with initialization.

```csharp
var tag = new Tag("MyTag", 123);
var dataType = new DataType("MyCustomType");
var task = new Task("PeriodicTask", TaskType.Periodic)
```

And some types will have factory methods that help even further. `Tag` contains a fluent builder API that makes
creating and configuring tag values much easier.

```csharp
var tag = Tag.Create("SomeTimer")
    .WithValue<TIMER>(t => 
    {
        t.PRE = 5000;
        t.DN = true;
    })
    .WithDescription("This is a fluent builder...")
    .ReadOnly()
    .Build();
```

> [!NOTE]
> When a component or element is created in memory, it is not attached to an L5X until you add it to an appropriate
> container collection (Tags, DataType, Modules, etc.). This is important to remember because some navigation
> properties will return null if the object is not part of the XML hierarchy.

### Modifying Components

L5Sharp offers various ways to configure or update components. At the simpliest level, you can directly set
properties of a component as needed.

```csharp
var tag = new Tag();

tag.Value = 50;
tag.Description = "Updated tag description";
tag.ExternalAccess = Access.ReadOnly;
tag.Usage = TasUsage.Public;
tag.AliasFor = "OtherTag.Member"
```

> [!WARNING]
> L5Sharp.Core is not strict in terms of property validation. In most cases, you can set a property on a component to
> any given value (acceptable by the value type) and attempt to import it into Studio. Studio will validate certain
> properties but be relaxed on others. For this reason, it's hard to fully perform validation ahead of time
> without manually importing permutations of content to see how Studio handles it. When we do find things Studio is
> strict on, we attempt to mimic that in the object construction and hide the details, such that the API can remain
> clean and simple.

All components contain methods for cloning or duplicating the instance with updated values. This is a key part
of the library since in many cases, you want to copy existing templates and update only a few properties.

```csharp
// Create a direct clone of the current object with no changes.
var clone = tag.Clone();

// Create a duplicate with updated config. This method will also add the duplicate to the L5X.
var duplicate = program.Duplicate(p => 
{
    p.Name = "Program_02";
    p.Description = "This is a different instance";
    p.Replace("Tag_01", "Tag_02"); //This method will perform find/replace of text in the new object.
});
```

Components also support "self-referencing" collection methods like `AddAfter`, `AddBefore`, `Remove`, and `Replace`.
These functions operate relative to the current instance and require the element to be attached to a parent
container or L5X. These methods mostly make use of the underlying `XElement` methods which do the same thing.

```csharp
// Add a new object after current instance in the document.
rung.AddAfter(new Rung("XIC(SomeTag.12)OTE(AnotherTag)"));

// Replace current instance with new one.
tag.Replace(new Tag { Name = "UpdatedTag", Value = 123, Description = "Completely new instance"});

// Removes this instance from the L5X or parent container.
routine.Remove()
```

All `LogixContainer<T>` collection also support adding, removing, and updating elements as you would expect.

```csharp
// Add a new component to a container
project.Tags.Add(new Tag { Name = "MyTag", Value = 100 });

// Remove a component from a container
project.Tags.Remove("OldTag");

// Bulk update components
project.Tags.Update(
    t => t.DataType == "TIMER", //update condition
    t => t.Description = "Updated TIMER description" //update action
);
```

Remember that all changes are directly applied to the underlying XML content. Once changes are complete, you can save
the L5X to your desired output.

```csharp
project.Save("Updated.L5X");
```

### Tag Data

### Controller Instance

The `L5X` class also contains access to the top level `Controller` object. This object contains all the high level
controller or project-wide settings that are configurable in Studio. The controller will also contain the same
reference to the component collections mentioned above.

```csharp
var controller = porject.Controller;

var name = controller.Name;
var processor = controller.ProcessorType;
var revision = controller.Revision;
var commPath = controller.CommPath;
var tags = controller.Tags;
// etc.
```

> [!NOTE]
> When an L5X is loaded into memory, this library will "normalize" the XML structure by inserting a root Controller
> element and all nested component collection elements (if not already present). This ensures that we can access these
> collection properties without running into exceptions.

### Partial Export Files

Not all L5X projects are complete project files. Some can be partial import/export files that
target specific components (Module, DataType, etc.). To inspect the "context" of a given L5X file, access the
`Content` property of the root L5X.

```csharp
// Tells you whether this is a partial export or a complete project export.
var isPartial = project.Content.ContainsContext;

// The name and type of the target component in the export file.
var targetName = file.Content.TargetName;
var targetType = file.Content.TargetType;
```

### Importing Content

## Components

L5Sharp.Core provides strongly typed representations of all major Logix component types found in L5X files.
Each component type offers intuitive properties and methods for inspection and modification, allowing you to work
with Logix project elements as native C# objects. The following sections will outline some of the key components
and common usage patterns.

### Working with Tags

## Architecture

L5Sharp.Core can be viewed in simple terms as a wrapper around LINQ to XML and the Rockwell XML schema for the
L5X import/export files. Internally, all classes wrap an `XElement` and represent some segment of XML. The members of
these classes are not much more than get/set accessors on the underlying elements and attributes that make up the XML
segment. This means L5Sharp.Core is stateful and lazy in that deserialization is defered until the user calls a specific
property or method. Deserializing a `LogixElement` is not much more than calling the constructor that initializes
the underlying XML elememnt.

This library has several key abstractions that may be useful to understand. Ultimately, they are just means of code
organization, and most users of this library will not interact with the base classes, but knowing the type hierarchy can
help solidify the mental model of the library before using it.

### LogixElement

The `LogixElement` class and corresponding `ILogixElement` interface are the core types of the library. They
contain the internal XElement object that all derived types can interact with. This class contains all the protected
getter and setter logic that makes the XML interaction in derived type much simpler. This type also exposes the
`Serialize()` method that will return the underlying XElement. Users can leverage this to write custom extensions that
interact with the internal XML structure. "Serialization" in this library is just a matter of returning the underlying
data store and is not actively transforming to a different format.

### LogixObject

The `LogixObject<TObject>` class derived directly from `LogixElement` and adds self-referential mutation methods such as
adding, removing, replacing, and duplicating other objects of the same type in the L5X. These methods basically leverage
the power of the underlying XElement and expose similar functions to help users mutate the object collections. This
class takes the generic type of the implementing class to make it type safe.

### LogixEntity

A `LogixEntity<TEntity>` (implementing `ILogixEntity`) is a type of element in the L5X that can be identified by some
unique identifier (name, number, etc.). This type class adds a couple key properties, `Reference` and `Scope`. These
properties identify where in the L5X document they exist. This is the primary type of the internal indexing code that
is discussed later.

### LogixComponent

The `LogixComponent<TComponent>` class implements `ILogixComponent` and adds property common to the main top level
component classes in the library. Component class include `Tag`, `DataType`, `Program`, etc. All components can be
identified by a name property. This class also adds functions for finding references, exporting the instance to
a new L5X

### LogixCode

## Feedback & Support

We welcome your feedback! Please report bugs or request features via
our [GitHub Issues](https://github.com/tnunnink/L5Sharp/issues) page.

## License

L5Sharp.Core is licensed under the [MIT License](https://github.com/tnunnink/L5Sharp/blob/master/LICENSE).
