# L5Sharp
A C# library for interacting with Rockwell's L5X import/export files.

## Purpose
The purpose of this library is to offer a reusable, strongly typed, intuitive API
over Rockwell's L5X schema, allowing developers to quickly modify or generate L5X content,
that be used to build PLC projects files. In doing so, this library can aid in 
creation of tools or scripts that automate the PLC development, maintenance, or testing processes
for automation engineers.

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
Get started by querying any type across the L5X using the `Find<T>()` method.
```csharp
var tags = content.Find<Tag>();
```
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
The following set of examples demonstrate these features using the Tags container.

###### Get All
Gets a list of all tags.
```c# 
var tags = content.Tags.ToList();
```
>[!NOTE]
>`Tags` on the root `LogixContent` class is controller tags only.
> To get program specific tags, access the `Tags` container of a 
> specific `Program` component.
###### Get Single
Get a tag by name.
```c#
var tag = content.Tags.Get("MyTag");
```
Get a tag by index.
```c#
var tag = content.Tags[4];
```
Find a tag by name.
```c#
var tag = content.Tags.Find("SomeTag");
```
>[!NOTE] 
> Using `Find` will not throw an exception if the specified component is not found in
> the L5X. Rather, it will simply return `null`.
###### Filter
Perform complex filtering using LINQ.
```c#
var tags = content.Tags.Where(t => t.DataType == "TIMER" && t.Dimensions.IsEmpty && t["PRE"].Value >= 5000);
```
###### Add
Adds a new component to the container.
```c#
var tag = new Tag { Name = "MyTag", Value = 100 };
content.Tags.Add(tag);
```
Add a new list of components to the container.
```c#
content.Tags.AddRange(new List<Tag>
{
    new() { Name = "tag01", Value = 100 },
    new() { Name = "tag02", Value = 200 },
    new() { Name = "tag03", Value = 300 }
});
```
###### Update
Updating properties of a component will directly update the underlying L5X content.
```c#
var tag = content.Tags.Get("MyTag");
tag.Value = 1234;
tag.Description = "This is a tag's description";
```
You may also want to apply and update to all components in a collection.
```csharp
content.Tags.Update(t => t.Comment = string.Empty);
```
Or better yet, update only components that satisfy a condition.
```csharp
content.Tags.Update(t => t.DataType == "MyType", t => t.Comment = "This is an instance ot MyType");
```
###### Insert
Insert a component at a specific position in the container.
```c#
content.Tags.Insert(3, new new Tag { Name = "MyTag", Value = 100 });
```
###### Remove
Remove a component by name.
```c#
content.Tags.Remove("MyTag");
```
Remove all components that satisfy a condition.
```csharp
content.Tags.Remove(t => t.DataType == "TypeName");
```
###### Save
Saving will write the current underlying L5X content to the specified file.
```c#
content.Save("C:\PathToMyOutputFile\FileName.L5X");
``` 

## Tag Data

## Extensions


## References
For more information regarding the structure and content of Rockwell's L5X file,
see [Logix 5000 Controllers Import Export](../refs/Logix 5000 Controllers Import Export.pdf)