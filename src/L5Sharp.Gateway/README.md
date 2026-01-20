# L5Sharp.Gateway

A .NET library providing PLC communication capabilities for Rockwell Automation Logix controllers through tag read/write
operations, integrating [L5Sharp](https://github.com/tnunnink/L5Sharp)
with [libplctag](https://github.com/libplctag/libplctag).

## Overview

L5Sharp.Gateway enables real-time communication with Allen-Bradley PLCs by bridging the high-performance
libplctag library with the rich type system of L5Sharp. While other wrappers exist, L5Sharp.Gateway is uniquely
designed to map PLC data directly to L5Sharp tag objects—whether they are extracted from an L5X project file or defined
dynamically in memory. This integration simplifies the API and provides out-of-the-box data mapping for complex
Logix structures.

## Motivation

The primary driver for L5Sharp.Gateway was to address the "data mapping gap" found in existing PLC libraries.
Most libraries excel at reading individual atomic tags (DINTs, REALs) but offer little support for complex structures.
To work with a UDT or a Predefined type (like a TIMER), developers are typically forced to manually map raw byte arrays
onto C# classes. This process is error-prone because Logix data packing rules such as member alignment and padding
differ significantly between UDTs and Predefined structures. Unpacking these types correctly often requires knowledge
of internal Logix memory layouts or tedious reverse engineering.

L5Sharp.Gateway eliminates this burden. Instead of manual byte-shuffling, developers work with strongly typed Tag
objects that handle serialization and deserialization automatically. By leveraging source-generated types for standard
Logix structures (TIMER, COUNTER, etc.) and providing tools to generate types for custom UDTs, the library ensures that
PLC communication is IntelliSense-friendly, type-safe, and free of boilerplate mapping code.

This provides the best of both worlds: the proven reliability of libplctag combined with the sophisticated modeling
of L5Sharp, resulting in cleaner, more maintainable code and significantly reduced development time.

## Features

- **Real PLC Communication**: Read and write tag data from/to physical controllers via libplctag
- **L5Sharp Integration**: Leverage L5Sharp.Core tag definitions and type system
- **Protocol Support**: Compatible with `ab_eip` protocol for ControlLogix, CompactLogix, and Micro800 PLCs
- **Virtual Tag Service**: In-memory service for testing without hardware
- **Tag Operations**: Single and bulk tag read/write capabilities
- **Tag Watch**: Monitor updates for specific tags as changes occur in the PLC
- **Async API**: All operations use modern Task-based methods
- **Result Pattern**: Get aggregated tag response object, similar to an HTTP client.
- **Type Safety**: Strongly typed tag data with automatic serialization

## Installation

L5Sharp.Gateway is available as a NuGet package and can be installed using your preferred package manager.

```powershell
Install-Package L5Sharp.Gateway
```

## Usage

The primary interface for the library is the `PlcClient` object. Create a new client by providing
the IP and slot of the PLC to connect to.

```csharp
using var client = new PlcClient("10.10.10.10", 1);
```

### Tag Reading

Use the client object to read a known tag by name and type.

```csharp
var response = await client.ReadTag<DINT>("MyDintTag");
```

> [!TIP]
> To read program tags, use the "Program:" prefix. Example: `Program:SomeProgram.MyDintTag`

Or get the tag from an existing `L5X` and pass it to the client.
Note that in the following example, we don't need to know the type of the tag since it is embedded in the L5X.

```csharp
var content = L5X.Load(@"C:\Path\To\MyFile.L5X");
var tag = content.Tags.Get("SomeTag");
var response = await client.ReadTag(tag);
``` 

Inspect the `TagRespone` object of the completed operation. The `Tag` object
that was provided or created will now have the updated data if the operation succeeded.

```csharp
Console.WriteLine($"Success: {response.Success}");
Console.WriteLine($"Result: {response.Result}");
Console.WriteLine($"Timestamp: {response.Timestamp}");
Console.WriteLine($"Duration: {response.Duration}");
Console.WriteLine($"Value: {response.Tags.First().Value}");
```

Another spot where this library shines is reading collections of tags. Since we can query an L5X using LINQ and pass
that collection to the client, we never even need to know the tag name or type at all.
For example, we can read all tags in a certain program:

```csharp
var tags = content.Query<Tag>(t => t.Scope.Container == "MyProgram");
var response = await client.ReadTags(tags);
```

### Tag Writing

Use the client object to write a known tag by name and type.

```csharp
var response = await client.WriteTag<REAL>("MyRealTag", 1.34);
```

For complex data structures, you can use the overload that takes a update action for the specified type.

```csharp
var response = await client.WriteTag<TIMER>("SomeTimer", d =>
{
    d.PRE = 12345;
    d.ACC = 123;
    d.EN = true;
});
```

Or pass in your own `Tag` object to write. We can either create an in-memory tag instance or
get an existing one from an `L5X` file.

```csharp
// Create tag in memory using builder API. 
// Use the "Program" prefix to specify this as a program tag.
var tag = Tag.Create("Program:SomeProgram.SomeTag")
    .WithValue(123)
    .Build();

var response = await client.WriteTag(tag);
```

### Tag Response

All read/write operations will return a single aggregate `TagResponse` object providing details on the result of the
operation.
The response object contains the following data:

- **Success**: A `bool` flag indicating whether the operation completed without errors.
- **Status**: A `TagStatus` value indicating the first error encountered if any occurred, or an "Ok" status otherwise.
- **Timestamp**: A `DateTime` value indicating the time at which the response was generated.
- **Duration**: A `TimeSpan` value indicating how long the operation took to process.
- **Tags**: The collection of resulting `Tag` instances with updated data.
- **Errors**: A collection of `TagError` objects containing a tag name and corresponding error.

### Tag Monitoring

The libplctag library lets you configure tags for periodic read operations to stream data from the controller.
This library leverages that feature using a tag watch API. To monitor a tag for changes,
call `WatchTag` and provide a callback delegate.

```csharp
var tag = Tag.New<DINT>("MyDintTag");

using var writeToConsole = await client.WatchTag(tag, t =>
    Console.WriteLine($"Tag: {t.Name} | Value: {t.Value}")
);
```

This will stream updated tag data into the provided tag instance while the watch is in memory.
The callback is invoked with the updated tag instance. The watch methods return an `IDisposable`
which will stop polling when disposed.

### Tag Management & State

L5Sharp.Gateway uses a stateful API design. The `Tag` instances you pass to the client are the same instances
that get updated when an operation completes. If you call a method that just takes a tag name and data type, the client
will create a new tag instance to use as the buffer for read/write operations and return that in the response.

The client will create and cache internal tag handles after each read/write operation for performance reasons 
but will not hold onto the provided tag instances. Once the client is disposed of, all handles are released. However,
for the tag watch feature, the client will create an internal tag watch structure which contains the reference to the 
Tag to update. This means that while a tag is being watched, that instance will stay in memory until the watch
for the tag is disposed of. This ensures that if you have a Tag bound to a UI element or a logic engine, it stays synchronized with the PLC 
automatically after a read operation.

### Client Configuration

Users can configure the client using the `PlcOptions` object. Along with IP and slot, you can configure various other
settings, such as timeout and read interval.

```csharp
var options = new PlcOptions
{
    IP = "10.10.10.10",
    Slot = 2,
    // Duration in milliseconds before timeout error occurs. Default is 5000
    Timeout = 30000,
    // Rate in milliseconds at which to poll for tag updates (only applies to Watch API). Default is 1000.
    ReadInterval = 100,
    // Which result statuses to throw exceptions for. Default is none.
    ThrowOn = { TagStatus.BadData, TagStatus.BadConnection, TagStatus.Timeout }
};

// Create the client with the configured options.
using var client = new PlcClient(options);
```

### Tag Service

This library contains an interface wrapper called `ITagService` that mimics the native library API
(but is reduced to only the methods that are used by PlcClient). This lets us inject a mock service implementation
to the client in cases where we don't have PLC hardware available. This library also includes a default
`VirtualTagService` implementation that mimics the native service, but uses a provided L5X as the backing store
for reading/writing tag data. When using the virtual tag service, all operations will effectively read from or write
to the underlying L5X, which would in theory mimic whatever PLC you would be communicating with.

```csharp
// This creates a new virtual tag service by loading the specified L5X and confiuring a fake latency duration.
var tagService = VirtualTagService.Upload(@"C:\Path\To\MyFile.L5X", TimeSpan.FromMilliseconds(10));

// You can inject this or any other mock service into the client constructor.
using var client = new PlcClient(options, tagService);
```

## Feedback & Support

We welcome your feedback, bug reports, and feature requests to help improve L5Sharp.Gateway.

### Reporting Issues

If you encounter a bug or have a feature request, please open an issue on our GitHub repository:

- **GitHub Issues**: [L5Sharp Issues](https://github.com/tnunnink/L5Sharp/issues)

When reporting issues, please include:

- A clear description of the problem or feature request
- Steps to reproduce (for bugs)
- Expected vs actual behavior
- Your environment details (PLC model, .NET version, library version)
- Sample code or L5X files if applicable

### Contributing

We appreciate contributions from the community! If you'd like to contribute:

- Fork the repository and create a feature branch
- Submit pull requests with clear descriptions of changes
- Follow the existing code style and conventions
- Include tests for new functionality

## License

L5Sharp.Gateway is licensed under the MIT License – see
the [LICENSE](https://github.com/tnunnink/L5Sharp/blob/master/LICENSE) file for details.

## Third-Party Licenses

The library uses [libplctag](https://github.com/libplctag/libplctag) for PLC communication, which is licensed under the
Mozilla Public License 2.0 (MPL 2.0).


