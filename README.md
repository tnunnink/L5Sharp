

# L5Sharp

C# library and API for querying and manipulating Rockwell's L5X export files.
The goal of this project was to provide a simple and and reusable library for 
interacting with L5X file to aid in the creation of tools that automate tasks 
related RSLogix 5000 PLC development. 

This project is still under development. 
If you would like to contribute or have feedback, please reach out.
If you like the progress being made, please leave a star. Thanks!




## Overview

The main entry point to the L5X is the LogixContent class. 
Use the factory methods to `Load` to load a file or `Parse` to parse a string,
exactly how you would with XDocument.
```c#
var content = LogixContent.Load("C:\PathToMyFile\FileName.L5X");
```

### Querying Content 
Once the LogixContent is created, you can you the component collection methods
to get access to most of the primary L5X components, 
such as `Tag`, `DataType`, `Module`, `Program`, and more. 
The following shows some simple querying via the tags component collection interface.
```c#
//Get all controller tags. 
var controllerTags = content.Tags();

//Get all in a program by providing a scope name.
var programTags = content.Tags("MainProgram");

//Get a tag by name.
var myTag = content.Tags().Find("MyTag");

//Use LINQ to query further.
var timersInMaingProgram = content.Tags("MainProgram").Where(t => t.DataType == "TIMER");
```
### Modifying Content
Modifying components is simple as well. 
The same component collection interface offers methods for adding,
removing, and replacing component. 

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

### Query Everywhere
Tags are unique components in that they have different scopes. 
We have to specify a scope to know where to modify the existing collection.
For example, if you call `Tags().Add(tag)`, where do we add the tag?

But what if I just want to query across the entire file, how do I do that?
The answer is use the `Query<T>()` method and specify the type to query as the 
generic argument.
```csharp
var allTagsInFile = content.Query<Tag>();
```
NOTE: `Query<T>()` just returns an `IEnumerable<T>`, so it is essentially read only.

### Create Your Own Entities
I didn't want to lock use down to just types provided in the library. 
If you want to create your own entity type to represent some data of the L5X content,
all you need to do is create the class and implement am `ILogixSerializer<T>` for that type.

Here is an example. First create some class.
```csharp
public class TagType
{
    public string TagName { get; set; }
    public string TagType { get; set; }
}
```
Then create a class that implements ILogixSerializer.
```csharp
public class TagTypeSerializer : ILogixSerializer<TagType>
{
    public XElement Serialize(TagType obj)
    {
        var element = new XElement("TagType");
        element.AddValue(obj, t => t.TagName);
        element.AddValue(obj, t => t.TagType);
        return element
    }
    
    
    public TagType Deserialize(XElement element)
    {
        return new TagType
        {
            TagName = element.GetValue<string>("Name");
        }
    }
}
```



Since you can specify any typ using Query(), you can create you 


## Documentation

[Logix 5000 Controllers Import/Export](https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf)

