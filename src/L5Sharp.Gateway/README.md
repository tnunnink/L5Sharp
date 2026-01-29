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
the IP address and slot number of the Logix PLC to connect to.

```csharp
using var client = new PlcClient("10.10.10.10", 1);
```

This object implements `IDisposable`. Once disposed of, all internal tag handles and resources will be
released automatically. Each `PlcClient` represents a connection to a single PLC. Use multiple clients
if you want to connect to different PLC devices or if you want to poll the same PLC at different rates
(polling is discussed later).

### Reading Tags

Use the client object to read a known tag by name and type.

```csharp
var result = await client.ReadTag<DINT>("MyDintTag");
```

This works for both atomic and complex typed tags.

```csharp
var result = await client.ReadTag<TIMER>("MyTimerTag");
```

To read a program scoped tag, prepend the `Program:{ProgramName}` specifier.

```csharp
var result = await client.ReadTag<REAL>("Program:MainProgram.SomeTag");
```

You can also read nested members of a complex structure directly. Specify the base structure type and
the tag name path to the member to read.

```csharp
var result = await client.ReadTag<TIMER>("MyTimerTag.PRE");
```

The previous example requires knowledge of the base tag to create a valid tag data element internally.
When the method returns, the result will reference the member tag that was specified, and that member tag object
will have access to the base complex tag (`TIMER` in this example).

It is also possible to read a nested member of any given structure by specifying the type of the member as
opposed to the type of the base tag as shown below.

```csharp
var result = await client.ReadTag<DINT>("MyTimer.PRE")
```

> [!NOTE]
> The previous example will run without error and contain the correct tag value when it returns.
> However, the tag element it creates internally will be "invalid" in the sense that the base
> name will be `MyTimer.PRE` and it will contain a simple atomic data element with no access to the
> base structure tag. This is because there is not enough information here to construct the complex data element.
> This is only an issue if you then try to import this tag object as part of some other L5X file,
> as Logix will likely throw some error about the tag name being invalid. If you are just using this
> as a means to read the value from a PLC, then you likely won't have any issues.

All methods above will create a new `Tag` instance to hold the data read from the PLC. However, you can
get a tag instance from an existing `L5X` and pass it to the client as well.

```csharp
var content = L5X.Load(@"C:\Path\To\MyFile.L5X");
var tag = content.Tags.Get("SomeTag");
var result = await client.ReadTag(tag);
``` 

With this approach, we don't need to specify the type of the tag since it is embedded in the tag element.
This means you can also create an in memory `Tag` instance and pass it to the client to read.

```csharp
var tag = Tag.New<TIMER>("Program:SomeProgram.SomeTimer");
var result = await client.ReadTag(tag);
```

Another spot where this library shines is reading collections of tags. Since we can query an L5X using LINQ and pass
that collection to the client, we never even need to know the tag name or type at all.
For example, we can read all tags in a certain program as shown here.

```csharp
var tags = content.Query<Tag>(t => t.Scope.Container == "MyProgram");
var results = await client.ReadTags(tags);
```

### Writing Tags

Use the client object to write a value to a known tag.

```csharp
var result = await client.WriteTag("MyRealTag", 1.34);
```

You can also write complex tag structures.

```csharp
var result = await client.WriteTag("MyTimer", new TIMER { PRE = 5000, DN = true });
```

Or using the overload that takes an update action for the specified type.

```csharp
var result = await client.WriteTag<TIMER>("Program:SomeProgram.MyTimer", d =>
{
    d.PRE = 12345;
    d.ACC = 123;
    d.EN = true;
});
```

> [!Warning]
> When writing a complex tag structure, the entire tag data structure will be overwritten, regardless of
> which members are configured in the value or update delegate. This means any unspecified member will
> write a default data value. If you want to only write to specific tag members of a complex strucutre,
> either speciy the specific the member path or use the `UpdateTag` methods discussed later.

To write to a single nested member of a complex structure, you can specify the tag name path and value.

```csharp
var result = await client.WriteTag("MyTimer.PRE", 5000);
```

> [!NOTE]
> This approach is nice is that it only updates the `PRE` member of the base `MyTimer` tag structure.
> However, as with the read operation, the returned `Tag` instance in the result object will contain
> an invalid name ("MyTimer.PRE"). This is not an issue unless you do something with the resulting
> tag object (like try to import it into Studio).

As with read operations, you can pass in your own `Tag` object to write, either creating one in-memory or
getting an existing one from an `L5X` file. The following example uses the tag builder API to construct
the tag instance to write.

```csharp 
// Use the "Program" prefix to specify this as a program tag.
var tag = Tag.Named("Program:SomeProgram.SomeTag")
    .WithValue(123)
    .Build();

var response = await client.WriteTag(tag);
```

And finally, you can perform bulk tag writes using the `WriteTags` method of the client.

```csharp
var results = await client.WriteTags(
[
    Tag.New<DINT>("First"),
    Tag.New<REAL>("Second"),
    Tag.New<TIMER>("Third"),
]);
```

And since you can pass in any collection of tag objects, you can query any L5X using LINQ, update tag data,
and provide that collection to the client. This makes bulk writes pretty seamless.

```csharp
var content = L5X.Load(@"C:\Path\To\MyFile.L5X");
        
var tags = content.Query<Tag>()
    .Where(t => t.DataType == "ALARM_ANALOG" && t.Dimensions == Dimensions.Empty && t.Scope.IsController )
    .Update(t =>
    {
        t.Value.As<ALARM_ANALOG>().HHEnabled = true;
        t.Value.As<ALARM_ANALOG>().HHLimit = 100;
    });

var result = await client.WriteTags(tags);
``` 

### Updating Tags

While the `WriteTag` methods will completely replace the data structure of a tag, the `UpdateTag` methods will
let you update only specific members. There are a couple overloads available on the client.

Update the members of a complex tag by providing a collection of name/value pairs. These can be nested tag name
paths or immediate child tags.

```csharp
var result = await client.UpdateTag<TIMER>("MyTimer",
[
    ("PRE", 5000),
    ("DN", 1)
]);
```

Or provide an update action and use the generic type parameter to statically set members.

```csharp
var result = await client.UpdateTag<TIMER>("MyTimer", t =>
{
    t.PRE = 5000;
    t.DN = 1;
});
```

This is the same overload as the `WriteTag` method, but internally it will detect which members are updated and
only create write operations for those specific members.

> [!NOTE]
> Both `WriteTag` and `UpdateTag` methods don't read data back from the PLC after writing.
> This means the `Tag` instance in the result object will contain the same instance provided to the operation.

### Tag Result

This library uses the result pattern to return the result of an operation (read, write, update, etc.).
This object will contian the overall outcome, diagnostic metadata like timestamp and duration,
as well as the `Tag` instance containing the data associated with the operation.

The result object contains the following properties:

- **Success**: A `bool` flag indicating whether the operation completed without errors.
- **Status**: A `TagStatus` value indicating the minimum error value if any occurred, or an "Ok" status otherwise.
- **Timestamp**: A `DateTime` value indicating the time at which the response was generated.
- **Duration**: A `TimeSpan` value indicating how long the operation took to process.
- **Tag**: The `Tag` instances containing the updated data value.
- **Errors**: A collection of `TagError` objects containing a tag name and corresponding error.

This result object has a collection of errors because it will read each member of a complex tag structure.
Therefore, each operation can contain multiple "results" or errors. These are aggregated using the `Status`
property, which will return the minimum error code found in the error collection. If no errors exist,
then the status is OK and the operation was successful.

### Tag Results (Bulk Methods)

The bulk operations like `ReadTags` and `WriteTags` will return a composite or aggregate result object called
`TagResults`. This is a collection of `TagResult` objects with the same summary properties mentioned above.
This type simply aggregates the results for the bulk operation into a single structure. The main differece is that
`TagResults` contains a `Tags` collection property representing all the tags associated with the operation.

### Monitoring Tags

The libplctag library lets you configure tags for periodic read operations to stream data from the PLC at a
specified poll rate. This library exposes that feature through the client using the `MonitorTag` methods.

```csharp
using var monitor = await client.MonitorTag<DINT>("MyDintTag");
```

This method returns a `TagMonitor` object that will recieve updates on the tags it is monitoring.
You can access the monitored tags and other health-related information on the monitor object.

The monitor object contains the following properties:

- **IsActive**: A `bool` indicating that at least one tag in the monitor is still polling for updates.
- **Status**: A `TagStatus` that represents the minimum error for any tag in the monitor. If no errors, then `Ok`
  status.
- **Timestamp**: The `DateTime` of the last tag update received by the monitor.
- **Rate**: The `TimeSpan` representing the average rate at which tags are reveiving updates.
- **Updates**: An `int` representing the total number of updates per period that are
- **Tags**: The collection of `Tag` objects that are being monitored and updated.
- **Errors**: A collection of `TagError` containing errors for any member of the monitored tag collection.

The `TagMonitor` class implements `IDisposable` so that it can stop polling and clean up internal tag handles
once disposed of. While the monitor is in memory, all tags will continue to be updated with data from the PLC
at the poll rate configured by the `PlcClient` object that created the monitor.

> [!WARNING]
> Since the monitor is continuously updating data on the tag instance, attempting to write or manipulate the data could
> cause unexpected issues. Just be cautious of the fact that these tag objects should be considered read-only
> and "owned" by the monitor until it is disposed of. If you need to write data, then create a separate
> tag instance or use the tag name overloads of the client.

> [!WARNING]
> All tags in the monitor object are actually managed by the `PlcClient` that created the monitor. This means
> that if you dispose of the client object, all internal tag watches will get disposed and the monitor will
> become "Inactive."

> [!NOTE]
> Two different monitors can stream the same tag reference on the PLC client. The client will internally manage
> subscriptions and only stop polling for a given tag once all subscribers are disposed of.

To subscribe to updates to tags on the monitor, you can register callback functions for
the three following events.

- **OnUpdate**: Called when a tag value is read from the PLC, regardless of the value changing.
- **OnChange**: Called when a tag value is read from the PLC and the value has changed.
- **OnError**: Called when a tag encounters an error status when reading from the PLC.

```csharp
// Subscribe to all reads regarless of value change.
monitor.OnUpdate(result => 
{
    Console.WriteLine($"Tag: {result.Tag.TagName} | Value: {result.Tag.Value} | Status: {result.Statue}");
});
```

These callbacks all receive a `TagResult` contianing the result of the operation at the given interval. Each
result object will represent a single tag member that was read. This means if you are monitoring complex typed
tags, the callback will get invoked with each nested member of the structure.

If you want to subscribe to a specific tag member or base tag, there are overloads that take a tag name.

```csharp
monitor.OnChange("MyTimerTag.PRE", _ => 
{
    // This only gets called when the member tag "MyTimerTag.PRE" changes value.
});
```

Or aggregate the changes by subscribing to the base tag.

```csharp
monitor.OnChange("MyCustomTag", _ => 
{
    // This gets called when any member of the base tag changes value, but the TagResult object passed to 
    // the callback will reference the base tag as opposed to the nested member tag.
});
```

> [!WARNING]
> These callbacks are synchronous, so try to avoid heavy work in the callback to prevent backpressure on the
> event stream. Offload work to separate thread as needed.

### Polling Tags

Polling tags provides a simplified way to continuously read tag values at regular intervals without managing
monitor subscriptions or callback functions. Unlike the monitor feature which uses events, polling will continuously
read a tag value until a specified stop condition is met. This library offers a couple of different stop conditions.

Poll a tag for a specified duration (e.g., 30 seconds). This will read updates into the tag and return
after 30 seconds with the final result.

```csharp
var result = await client.PollTag<DINT>("MyTagName", TimeSpan.FromSeconds(30));
```

Poll a tag until its value satisfies a specified condition.

```csharp
var result = await client.PollTag<DINT>("MyTagName", d => d > 100);
```

> [!NOTE]
> You can provide a cancellation token if this needs to be aborted after some time period.

Poll a tag until a value condition is satisfied or a duration expires (whichever happens first).

```csharp
var result = await client.PollTag<DINT>("MyTagName", d => d > 100, TimeSpan.FromSeconds(30));
```

> [!NOTE]
> This overload will not throw an error when the duration expires. It's up to the caller to determine
> if the result matches the predicate specified.

These methods were intended for use cases like testing or writing procedural code that needs to first set a
tag value and then block continuation until some other tag reaches a certain state, which could take 
some arbitrary amount of time.

### Client Configuration

You can configure the client using the `PlcOptions` object. Along with IP and slot, the object contains properties
to configure various other settings, such as timeout and poll rate.

```csharp
var options = new PlcOptions
{
    IP = "10.10.10.10",
    Slot = 2,
    // Duration in milliseconds before timeout error occurs. Default is 5000
    Timeout = 30000,
    // Rate in milliseconds at which to poll for tag updates (only applies to MonitorTag and PollTag methods). Default is 1000.
    PollRate = 100,
    // Which result statuses to throw exceptions for. Default is none.
    ThrowOn = { TagStatus.BadData, TagStatus.BadConnection, TagStatus.Timeout }
};

// Create the client with the configured options.
using var client = new PlcClient(options);
```

### Tag Service

This library contains an interface wrapper called `ITagService` that mimics the native libplctag library API
(but is reduced to only the methods that are used by PlcClient). This lets us inject a mock service implementation
to the client in cases where we don't have PLC hardware available. This library also includes a default
`VirtualTagService` implementation that mimics the native service, but uses a provided L5X as the backing store
for all client operations. When using the virtual tag service, all operations will effectively read from or write
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


