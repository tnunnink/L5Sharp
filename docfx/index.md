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

The main entry point to the L5X is the `LogixContent` class. 
Use the factory methods `Load` to load a L5X file or `Parse` to parse a L5X string.
```c#
var content = LogixContent.Load("C:\PathToMyFile\FileName.L5X");
```

Query any type across the L5X using the `Find<T>()` method on the content class.
`Find<T>()` just returns an `IEnumerable<T>`, allowing for more complex queries
using LINQ and the strongly typed objects in the library. 
```csharp
var tags = content.Find<Tag>();
```
[!NOTE]
Ths above query will return all Tag elements found, including controller and all program tags.

## Usage
The `LogixContent` class contains `LogixContainer` collections for all L5X components, 
such as [Tag](xref:L5Sharp.Components.Tag), [DataType](xref:L5Sharp.Components.DataType),
[Moulde](xref:L5Sharp.Components.Module), and more. 
These classes expose methods for querying and modifying the collections
and components within the collections.

#### Get All Components
```c# 
var tags = content.Tags.ToList();
```
#### Get Component By Name
```c#
var tag = content.Tags.Find("MyTag");
```
#### Filter Components
```c#
var tags = content.Tags.Where(t => t.DataType == "TIMER" && t.Dimensions.IsEmpty && t["PRE"].Value >= 5000);
```
#### Add Component
```c#
var tag = new Tag { Name = "MyTag", Value = 100 };
content.Tags.Add(tag);
```
#### Update Component
```c#
var tag = content.Tags.Get("MyTag");
tag.Value = 1234;
tag.Description = "This is a tag's description";
```
#### Remove Component
```c#
content.Tags.Remove("MyTag");
```
#### Save Changes
```c#
content.Save("C:\PathToMyOutputFile\FileName.L5X");
``` 

See ... for more information.
