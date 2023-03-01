

# L5Sharp
A C# library and API for interacting with Rockwell's L5X import/export files.
The goal of this project was to provide a simple and reusable library for
querying and manipulating L5X files to aid in the creation of tools that
automate tasks related RSLogix 5000 PLC development.

## Getting started
This project is still under development and in a preview phase, but you can 
start using it by adding the package from Nuget.
```powershell
Install-Package L5Sharp
```
If you would like to contribute, have feedback or questions, please reach out!
If you are using or like the progress being made, please leave a star. Thanks!


## Overview
The following is a basic walk through of some of the high level features
ofr the library. I plan to create a more in depth wiki at some point, but for
now please refer to the following information to understand how to use the
library.

### Entry Point
The main entry point to the L5X is the `LogixContent` class. 
Use the factory methods `Load` to load a file or `Parse` to parse a string,
exactly how you would with `XDocument`.
```c#
var content = LogixContent.Load("C:\PathToMyFile\FileName.L5X");
```

`LogixContent` has a `L5X` property, which is essentially the root content element
of the document. So if you ever need access the XML directly, use that property.
It also contains the root content attributes, such as target type, target component,
software version, and others.
```csharp
var content = LogixContent.Load(Known.Test);

content.L5X.Should().NotBeNull();
content.L5X.SchemaRevision.Should().Be(new Revision());
content.L5X.SoftwareRevision.Should().Be(new Revision(32, 02));
content.L5X.TargetName.Should().Be("TestController");
content.L5X.TargetType.Should().Be("Controller");
content.L5X.ContainsContext.Should().Be(false);
content.L5X.Owner.Should().Be("tnunnink, EN Engineering");
content.L5X.ExportDate.Should().NotBeNull();
```

### Querying Content 
Once the LogixContent is created, you can you the component collection methods
to get access to most of the primary L5X components, 
such as `Tag`, `DataType`, `Module`, `Program`, and more. 
The following shows some simple querying via the tags component collection interface.
```c#
//Get all controller tags. 
var controllerTags = content.Tags();

//Get all tags in a program by providing a scope name.
var programTags = content.Tags("MainProgram");

//Get a tag by name.
var myTag = content.Tags().Find("MyTag");

//Use LINQ to query further.
var timersInMaingProgram = content.Tags("MainProgram").Where(t => t.DataType == "TIMER");
```
### Modifying Content
Modifying components is simple as well. 
The same component collection interface offers methods for adding,
removing, and replacing components. 

```csharp
//Add a new component.
var tag = new Tag
{
    Name= "NewTag";
    Data = new DINT();
};

content.Tags().Add(tag);

//Remove an existing component.
var result = content.Tags().Remove("MyTag");

//Repalce an existing component.
var result = content.Tags().Replace(tag);

//Save changes when done.
content.Save("C:\PathToMyOutputFile\FileName.L5X");
```

### More Querying
Tags are unique components in that they have different scopes. 
We have to specify a scope to know where to modify the existing collection.
For example, if you call `Tags().Add(tag)`, where do we add the tag?
This is why you have to specify a scope name for the tags collection when interacting with it.
The same applies to `Routine`.

But what if I just want to query across the entire file, how do I do that?
The answer is use the `Query<T>()` method and specify the type to query as the 
generic argument.
```csharp
var allTagsInFile = content.Query<Tag>();
```
NOTE: `Query<T>()` just returns an `IEnumerable<T>`, so it is essentially read only.


### Tag Data
Tag components also contain simple or complex data structures.
This library includes prebuilt `Atomic` and `Predefined` data structures
that allow in memory creation of tag data.

When creating a Tag component, initialize the `Data` property to get
a new instance of instantiated tag data for the type.

The following creates a tag with the predefined `TIMER` tag structure.
```csharp
var tag = new Tag
{
    Name = "TimerTag"
    Data = new TIMER()
};
```

You can then get members of the tag using the Member() or Members() methods.
```csharp
//Get the preset member of the timer tag.
var pre = tag.Member("PRE");

//Set the value of the DINT preset member.
pre.Data = 5000;

//Get all tag members.
var members = tag.Members();
```
When serialized back to the L5X, this component will include all the 
tag data you have set on the component. Nice!

### Custom Data Types
The goal is to continue adding all predefined data structures, but if
one you require is missing, or if you have a user defined type that
you want to create, you can add them yourself.

Just inherit from `StructureType` and start creating type members like so.
```csharp
/// <summary>
/// A test type used to test nested complex data structure code
/// </summary>
public class MyNestedType : StructureType
{
    public MyNestedType() : base(nameof(MyNestedType))
    {
    }

    public override DataTypeClass Class => DataTypeClass.User;

    /// <summary>
    /// A simple boolean member
    /// </summary>
    public BOOL Indy { get; set; } = new();

    /// <summary>
    /// A string member
    /// </summary>
    public STRING Str { get; set; } = new();

    /// <summary>
    /// A nested timer member
    /// </summary>
    public TIMER Tmr { get; set; } = new();

    /// <summary>
    /// A nested user defined type
    /// </summary>
    public MySimpleType Simple { get; set; } = new();

    /// <summary>
    /// A nested array of atomic values. 
    /// Using this Logix.Array will create array filled with instantiated types.
    /// This will aviod issues with the structure type members collection containing
    /// null data for you members.
    /// </summary>
    public BOOL[] Flags { get; set; } = Logix.Array<BOOL>(10).ToArray();

    /// <summary>
    /// A nested array of structure types.
    /// </summary>
    public MESSAGE[] Messages { get; set; } = Logix.Array<MESSAGE>(10).ToArray();
}
```
IMPORTANT: When creating custom types, it is important to make them properties,
and initialize them straight away. Internally, StructureType will generate the 
Members collection using reflection and adding all ILogixType members. If not initialized,
Members will contain null instances of the data types, which may cause you some 
runtime exceptions when trying to access their members.

## Documentation
For further reading on Rockwell's import/export features, 
see the following published document.
[Logix 5000 Controllers Import/Export](https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf)

