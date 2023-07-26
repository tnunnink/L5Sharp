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
>NOTE:
>`Find<T>()` returns an `IEnumerable<T>`, allowing for complex queries
using LINQ and the strongly typed objects in the library.
> Since `Find<T>()` queries the entire L5X for the specified type, the above query
> will return all **Tag** components found, including controller and program tags.

## Usage
The following is some basic examples of how you can use this library
to query and modify the L5X content.

### Querying 
Once the LogixContent is created, you can use the container properties
to get access to all of the primary L5X components, 
such as `Tag`, `DataType`, `Module`, `Program`, and more. 
The following shows some simple querying via the `Tags` component container.
```c#
//Get all controller tags. 
var tags = content.Tags.ToList();

//Get a tag by name.
var tag = content.Tags.Find("MyTag");

//Use LINQ to query further.
var results = content.Tags.Where(t => t.DataType == "TIMER");
```
### Modifying
Modifying components is simple as well. 
The same component collection interface offers methods for adding,
removing, and replacing components. 

```csharp
//Add a new component.
var tag = new Tag { Name = "MyTag", Value = 100 };
content.Tags.Add(tag);

//Remove an existing component.
var result = content.Tags.Remove("MyTag");

//Repalce an existing component.
var result = content.Tags.Find("MyTag").Replace(tag);

//Save changes when done.
content.Save("C:\PathToMyOutputFile\FileName.L5X");
```


## Documentation
See my project GitHub Page [here](https://tnunnink.github.io/L5Sharp/index.html) for more in depth articles and API documentation.

