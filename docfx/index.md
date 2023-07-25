# L5Sharp
A C# library for interacting with Rockwell's L5X import/export files.
The goal of this project was to provide a simple and reusable library for
querying and manipulating L5X files to aid in the creation of tools that
automate tasks related RSLogix5000 PLC development.

If you would like to contribute, have feedback or questions, please reach out!
If you are using or like the progress being made, please leave a star. Thanks!

## Quick Start
Install package from Nuget.
```powershell
Install-Package L5Sharp
```

The main entry point to the L5X is the `LogixContent` class. 
Use the factory methods `Load` to load a L5S file or `Parse` to parse a L5X string.
```c#
var content = LogixContent.Load("C:\PathToMyFile\FileName.L5X");
```

Quickly query any type using the `Find<T>()` method on the content class.
```csharp
var tags = content.Find<Tag>();
```
NOTE: `Find<T>()` just returns an `IEnumerable<T>`, so it is essentially read only,
but allows you to form more complex queries using LINQ and the strongly typed
components in the library. To mutate components, see the following section.

### Components
Query or mutate components in the file using the top level `LogixContainer` collections,
 such as `Tag`, `DataType`, `Module`, `Program`, and more. 

The following shows some simple querying via the controller tags container:
```c#
//Get all controller tags. 
var controllerTags = content.Tags;

//Get a tag by name.
var myTag = content.Tags.Find("MyTag");

//Use LINQ to query further.
var timerTypeTags = content.Tags.Where(t => t.DataType == "TIMER");
```

The following shows some simple modifications via the controller tags container:
```csharp
//Add a new component.
var tag = new Tag { Name = "MyTag", Value = 100 };
content.Tags.Add(tag);

//Remove an existing component.
content.Tags.Remove("MyTag");

//Repalce an existing component.
content.Tags.Find("MyTag").Replace(tag);

//Save changes when done.
content.Save("C:\PathToMyOutputFile\FileName.L5X");
``` 

See ... for more information.
