[![MIT License](https://img.shields.io/apm/l/atomic-design-ui.svg?)](https://github.com/tnunnink/L5Sharp/blob/main/LICENSE)


# L5Sharp

C# object oriented API for querying and manipulating RSLogix L5X import/export files. The primary purpose of this library is to aid in the creation of tools that automate tasks related RSLogix PLC development. This project is still under development. If you would like to contribute or have feedback, please reach out. If you like the progress being made, please leave a start. Thanks!


## Usage/Examples

The main entry point to the L5X is the LogixContext class. Create an instance of the context by supplying the path to your L5X export.
```c#
var context = new LogixContext("FileName.L5X");
```

The context contains repositories that are used to read from and write to the L5X file.
```c#
var tag = context.Tags.Get("MyTagName");

var tags = context.Tags.GetAll();

context.Tags.Add(Tag.Create<Dint>("NewTag"));
```

## DataTypes
You can create your own user defined data types.
```c#
var dataType = new DataType("MyUserDefined", "This is a user defined type",
                new List<IMember<IDataType>>
            {
                Member.Create<Bool>("Member01"),
                Member.Create<Int>("Member02"),
                Member.Create<Dint>("Member03"),
                Member.Create<Real>("Member04")
            });
```

## Tags
Create new tags using static factory methods. Tags can be strongly typed using generics to allow the developer to gain access to the underlying type members and avoind using string arguments.

Use the Create method to create a new tag with default parameters.
```c#
var tag = Tag.Create<Timer>("NewTag");

tag.SetMember(t => t.PRE, new Dint(5000));
```

Use the Build method to build up a tag using a fluent API.
```c#
 var tag = Tag.Build<Sint>("NewTag")
                .WithDimensions(10)
                .WithRadix(Radix.Hex)
                .WithAccess(ExternalAccess.ReadOnly)
                .WithDescription("This is a test tag")
                .Create();
```


## Documentation

[Logix 5000 Controllers Import/Export](https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf)

