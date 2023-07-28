# L5Sharp
A library for intuitively interacting with Rockwell's L5X import/export files.

## Purpose
The purpose of this library is to offer a reusable, strongly typed, intuitive API
over Rockwell's L5X schema, allowing developers to quickly modify or generate L5X content.
In doing so, this library can aid in creation of tools or scripts that automate the PLC development, maintenance, 
or testing processes for automation engineers.

## Goals
The following are some high level goals this project aimed to accomplish.
1. Provide a simple and intuitive API, making the learning curve as low as possible.
2. Ensure performance as much as possible without sacrificing simplicity.
3. Make it easy and seamless to extend the API to support custom queries or functions.
4. Support strongly typed mutable tag data, so we can reference complex structures statically at compile time.

## Feedback
If you like or use this project, leave a star. If you have questions or issues, 
please post in the [issues](https://github.com/tnunnink/L5Sharp/issues) tab 
of the project repository. Any feedback is always appreciated. Enjoy!

## Quick Start
Install package from Nuget.
```powershell
Install-Package L5Sharp
```
Load an L5X file using the primary entry point `LogixContent` class.
```c#
var content = LogixContent.Load("C:\PathToMyFile\FileName.L5X");
```
Get started by querying any type across the L5X using the `Find<T>()` and LINQ extensions.
```csharp
var results = content.Find<Tag>()
                .SelectMany(t => t.Members())
                .Where(t => t.DataType == "TIMER")
                .Select(t => new { t.TagName, t.Comment, t.Value.As<TIMER>().PRE })
                .ToList();
```
The above query gets all tags and their nested tag members of type TIMER and returns the TagName,
Comment, and Preset value in a flat list.
>[!NOTE]
>`Find<T>()` returns an `IEnumerable<T>`, allowing for complex queries
using LINQ and the strongly typed objects in the library. 
> Since `Find<T>()` queries the entire L5X for the specified type, the above query
> will return all **Tag** components found, including controller and program tags.

## Usage
The `LogixContent` class contains `LogixContainer` collections for all L5X components, 
such as [Tag](xref:L5Sharp.Components.Tag), [DataType](xref:L5Sharp.Components.DataType),
[Moulde](xref:L5Sharp.Components.Module), and more. 
These classes expose methods for querying and modifying the collections and components within the collections.
The following set of examples demonstrate some of these features using the `Tags` container.

#### Querying
Component containers implement `IEnumerable<T>`, and hence support LINQ extensions such as `ToList()`,
`Where()`, and much more.

- Get a list of all components in a container like so:
```c# 
var tags = content.Tags.ToList();
```
>[!NOTE]
>`Tags` on the root `LogixContent` class represent controller tags only.
> To get program specific tags, access the `Tags` container of a 
> specific `Program` component.

- Perform complex filtering using LINQ expressions:
```c#
var tags = content.Tags.Where(t => t.DataType == "TIMER" && t["PRE"].Value >= 5000);
```

Aside from LINQ, the following are some built in ways to get or find components.

- Get a component at a specific index using the indexer property:
```c#
var tag = content.Tags[4];
```

- Get a component by name using `Get()` and specifying the component name:
```c#
var tag = content.Tags.Get("MyTag");
```
>[!WARNING]
> `Get()` will throw an exception if the component name was not found or more than one component with
> the specified name exists in the container. This is synonymous with `Single()` from LINQ.

- Find a component by name using `Find()` and specifying the component name:
```c#
var tag = content.Tags.Find("SomeTag");
```
>[!NOTE] 
> Using `Find` will not throw an exception if the specified component is not found in
> the L5X. Rather, it will simply return `null`. `Find()` is synonymous with `FirstOfDefault()` from LINQ.

#### Modifying
`LogixContainer` implement methods for mutating the collections as well. Create new component objects in memory, 
configure their properties, and add them to the container.

- Adds a new component to the container.
```c#
var tag = new Tag { Name = "MyTag", Value = 100 };
content.Tags.Add(tag);
```
>[!WARNING]
> Components are not validated as they are created or added to a L5X container. Therefore, adding
> duplicate component names or components with invalid property values may results in import failures.

- Add a new list of components to the container.
```c#
content.Tags.AddRange(new List<Tag>
{
    new() { Name = "tag01", Value = 100 },
    new() { Name = "tag02", Value = new TIMER { PRE = 2000 } },
    new() { Name = "tag03", Value = "This is a string tag value" }
});
```
- Updating properties of a component will directly update the underlying L5X content.
```c#
var tag = content.Tags.Get("MyTag");
tag.Value = 1234;
tag.Description = "This is a tag's description";
```
- You may also want to apply and update to all components in a collection.
```csharp
content.Tags.Update(t => t.Comment = string.Empty);
```
- Or better yet, update only components that satisfy a condition.
```csharp
content.Tags.Update(t => t.DataType == "MyType", t => t.Comment = "This is an instance ot MyType");
```
- Remove a component by name.
```c#
content.Tags.Remove("MyTag");
```
- Remove all components that satisfy a condition.
```csharp
content.Tags.Remove(t => t.DataType == "TypeName");
```
- Saving will write the updated L5X content to the specified file.
```c#
content.Save("C:\PathToMyOutputFile\FileName.L5X");
```

## Components

The following is a list of some of the _Logix_ components this library supports. 
- DataType
- AddOnInstruction
- Module
- Tag
- Program
- Routine
- Task
- Trend
- WatchList
- ParameterConnection

For more information for each
component, you can read the article [Components] or review the [Api] documentation.

## Tag Data
This library supports static access and in memory creation of complex tag data structures. 
The following example illustrates how this is done by creating a tag initialized with a `TIMER` structure,
and accessing it's `PRE` member statically.
```csharp 
//Create a TIMER tag.
var tag = new Tag { Name = "Test", Value = new TIMER { PRE = 5000 } };

//Get PRE value statically
var pre = tag.Value.As<TIMER>().PRE;

//Assert that the value is correct.
pre.Shoud().Be(5000);
```
This library also comes built in with all atomic logix types (BOOL, DINT, REAL, etc.) and some predefined 
logix types (TIMER, COUNTER, ALARM_ANALOG, PID, etc). You may also create your own user defined types to perform 
the same operations as shown above.

For more information on tag data and these complex logix type objects, see the article [Tag Data].

## Extensions
Extending this library and it's components or objects is fairly straight forward. We simply make use
of C# extension methods. Since most objects implement `ILogixSerializable`, you will have access to the underlying XML, 
This can be used as a means to write custom LINQ to XML queries or functions. 

The following is an example of using an extension method and LINQ to XML to add a query that gets all `DataType` 
components that are dependent on a specific data type name. In other words, data types that have members of the
specified data type name.

```csharp
public static IEnumerable<DataType> DependentsOf(this LogixContainer<DataType> dataTypes, string name)
{
    return dataTypes.Serialize().Descendants(L5XName.DataType)
        .Where(e => e.Descendants(L5XName.Member).Any(m => m.Attribute(L5XName.DataType)?.Value == name))
        .Select(e => new DataType(e));
}
```
Here you see we first call `Serialize()` to get the attached `XElement` object. We then perform a LINQ to XML query
and finally return a materialized list of `DataType` component objects.

For more information on extending the library, see the article [Extensions].

## References
For more information regarding the structure and content of Rockwell's L5X file,
see [Logix 5000 Controllers Import Export](../refs/Logix 5000 Controllers Import Export.pdf)