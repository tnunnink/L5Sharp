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

## Packages
You can consume the library via NuGet.
```powershell
Install-Package L5Sharp
```
>Previously I had two separate libraries but have since consolidated to a single package to avoid confusion and since
> I think most people would want all functionality anyway. `L5Sharp.Extensions` is no longer maintained.

## Quick Start
1. Install package from Nuget.
    ```powershell
    Install-Package L5Sharp
    ```
2. Load an L5X file using the `L5X` class static factory methods.
    ```c#
    var content = L5X.Load("C:\PathToMyFile\FileName.L5X");
    ```
3. Get started by querying elements across the L5X using the `Query()` methods and LINQ extensions. 
The following query gets all tags and their nested tag members of type TIMER and returns the TagName,
Description, and Preset value in a flat list ordered by TagName.
    ```csharp
    var results = content.Query<Tag>()
                .SelectMany(t => t.Members())
                .Where(t => t.DataType == "TIMER")
                .Select(t => new {t.TagName, t.Description, Preset = t["PRE"].Value})
                .OrderBy(v => v.TagName)
                .ToList();
    ```

    >`Query<T>()` returns an `IEnumerable<T>`, allowing for complex queries 
   > using LINQ and the strongly typed objects in the library. 
   > Since `Query<T>()` queries the entire L5X for the specified type, the above query 
   > will return all **Tag** components found, including controller and program tags.

## Usage
The following is some basic examples of how you can use this library
to query and modify the L5X content.

### Querying

##### Using Container Collections
Once the L5X is created, you can use the component properties to access the `LogixContainer` for the specified type.
This gets you simple collection APIs that allow you to query for component objects.

This library supports all primary components including the following:
- Controller
- DataType
- AddOnInstruction
- Module
- Tag
- Program
- Routine
- Task
- Trend
- WatchList

For example, the following shows some simple querying via the `Tags` component container.
```c#
//Get all controller tags. 
var tags = content.Tags.ToList();

//Get a tag by name.
var tag = content.Tags.Find("MyTag");

//Use LINQ to query further.
var results = content.Tags.Where(t => t.DataType == "TIMER");
```
##### Using Query Methods
The previous code will only return controller scoped tags since the `Tags` container on the
root L5X represents controller scoped tag elements. To get program tags we would 
have to access the `Tags` container on a specific program.
```csharp
//Get the first program in the L5X and find a tag called 'MyProgramTag'
var programTags = content.Programs.First().Tags.Find("MyProgramTag");
```
This is a little cumbersome. It would be nice to have a single way to query for all tags
across the file and filter those results as well. And there is using the `Query` methods. These generic methods
will return all instances of the type found in the file and allow you to further filter them using LINQ.
```csharp
//Gets all tags (controller, program, module/IO)
var allTags = content.Query<Tag>().ToList();

var programTags = allTags.Where(t => t.Scope == Scope.Program);
var ioTags = allTags.Where(t => t.Name.Contains(':'));
var readWriteTags = allTags.Where(t => t.ExternalAccess == ExternalAccess.ReadWrite);
var timerTags = allTags.Where(t => t.DataType == "TIMER");
```
##### Using Index Methods
Some L5X projects can contain a lot of `Tag` components especially when you consider all the nested
tag members as an individual tag that can be referenced in the project. 
If you have to perform continuous lookup of tag objects for some operation or task, that could be costly
or time intensive. We need a fast way to retrieve components, and this library offers that as well.

The following methods use an internal index or set of dictionaries to quickly find a component with by
name and type.
```csharp
//Find a data type component by name
var tag = content.Find<DataType>("MyUserDefined");

//Get a data type component by name 
//(the different here is Get throws an exception if not found)
var tag = content.Get<DataType>("MyUserDefined");

//Get a nested tag member 
//(we don't need to specify <Tag> since it is implied with the Tagname overload)
var tag = content.Find("MyTag.Member[1].SubMember.1");

//Gets all tags with name 
//(this could be controller and/or program tags)
var tags = content.All("ScopedTag").ToList();
```
The above calls all operate in constant time since the components are indexed using dictionaries. The L5X is 
only indexed when the first call to an index method such as `Find`, `Get` or `All` is made. The index is cached
so everytime after we don't need to recreate it. It will also be maintained internally as components are
added or removed.

### Modifying
Modifying components is simple as well.
The same component collection interface offers methods for adding,
removing, and updating components.

```csharp
//Add a new component.
var tag = new Tag { Name = "MyTag", Value = 100 };
content.Tags.Add(tag);

//Remove an existing component.
var result = content.Tags.Remove("MyTag");

//Repalce an existing component.
var result = content.Tags.Find("MyTag").Replace(tag);

//Configure individual properties.
var tag = content.Tags.Get("MyTag");
tag.Value = 100;
tag.Description = "This is an update";
tag.ExternalAccess = ExternalAccess.ReadOnly;
tag.Constant = true;

//Apply update funtion to all components that satisfy a condition.
content.Tags.Update(t => t.DataType == "TIMER", t => t.Description = "Updated TIMER description");

//Save changes when done.
content.Save("C:\PathToMyOutputFile\FileName.L5X");
```

## Documentation
See my GitHub Page [here](https://tnunnink.github.io/L5Sharp/index.html) for more in depth articles and API documentation.
The documentation is always a work in progress. If you think something is unclear or can be improved upon,
feel free to let me know in the [issues](https://github.com/tnunnink/L5Sharp/issues) tab of the project repository.

## Feedback
If you like or use this project, leave a star. If you have questions or issues,
please post in the [issues](https://github.com/tnunnink/L5Sharp/issues) tab
of the project repository. Any feedback is always appreciated. Enjoy!


