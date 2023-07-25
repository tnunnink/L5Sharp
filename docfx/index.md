# L5Sharp
A C# library for interacting with Rockwell's L5X import/export files.
The goal of this project was to provide a simple and reusable library for
querying and manipulating L5X files to aid in the creation of tools that
automate tasks related RSLogix5000 PLC development.

## Quick Start
Install package from Nuget.
```powershell
Install-Package L5Sharp
```
Load an L5X file using the primary entry point `LogixContent` class.
```c#
var content = LogixContent.Load("C:\PathToMyFile\FileName.L5X");
```
Query any type across the L5X using the `Find<T>()` method.
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
These classes expose methods for querying and modifying the collections
and components within the collections.

###### Get All
```c# 
var tags = content.Tags.ToList();
```
>[!NOTE]
>`Tags` on the root `LogixContent` class is controller tags only.
> To get program specific `Tags`, access the Tags collection of a 
> specific `Program` component.
###### Get By Name
```c#
var tag = content.Tags.Get("MyTag");
```
###### Filter
```c#
var tags = content.Tags.Where(t => t.DataType == "TIMER" && t.Dimensions.IsEmpty && t["PRE"].Value >= 5000);
```
###### Add
```c#
var tag = new Tag { Name = "MyTag", Value = 100 };
content.Tags.Add(tag);
```
###### Update
```c#
var tag = content.Tags.Get("MyTag");
tag.Value = 1234;
tag.Description = "This is a tag's description";
```
###### Remove
```c#
content.Tags.Remove("MyTag");
```
###### Save
```c#
content.Save("C:\PathToMyOutputFile\FileName.L5X");
``` 

See ... for more information.
