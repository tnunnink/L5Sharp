# L5Sharp

A .NET library for interacting with Rockwell's L5X import/export files.

## Overview

L5Sharp is designed to provide an intuitive and strongly-typed interface for working with Rockwell Automation's L5X
import/export files. This library enables developers and automation engineers to easily read, query, modify, and
generate L5X content programmatically.

## Features

- **Simple and Intuitive API**: Class and property names should be familiar to Rockwell PLC developers.
- **Multi-target Framework Support**: Compatible with .NET Standard 2.0 and .NET 8.0
- **Strongly-typed Component Model**: Work with tags, programs, rungs, and other Logix components in a type-safe manner
- **Powerful Querying Capabilities**: Leverage LINQ to perform complex queries across your L5X content
- **Efficient Component Indexing**: Fast lookups with indexed components for performance-critical operations
- **Component Modification**: Add, remove, update, or replace components with ease
- **Mutable Tag Data**: Reference and modify complex tag structures statically at compile time
- **Extensible Architecture**: Seamlessly extend the API to support custom queries or functions

## Installation

Install L5Sharp from NuGet:

```powershell
Install-Package L5Sharp
```

## Quick Start

### Load an Existing L5X File

``` csharp
// Load an L5X file
var content = L5X.Load("C:\\PathToMyFile\\FileName.L5X");
```

### Query L5X Components

``` csharp
// Get all controller tags
var tags = content.Tags.ToList();

// Find a specific tag by name
var myTag = content.Tags.Find("MyTag");

// Query all TIMER tags across the entire project
var timerTags = content.Query<Tag>()
    .Where(t => t.DataType == "TIMER")
    .ToList();

// Query nested tag members
var results = content.Query<Tag>()
    .SelectMany(t => t.Members())
    .Where(t => t.DataType == "TIMER")
    .Select(t => new {t.TagName, t.Description, Preset = t["PRE"].Value})
    .OrderBy(v => v.TagName)
    .ToList();
```

### Fast Component Lookup
To index the L5X on load or parse, you must supply the `L5XOptions.Index`.
```csharp
var content = L5X.Load("MyTestFile.L5X", L5XOptions.Index);
```
Then use `ILogixLookup` API `Get`, `TryGet`, `Find`, or `Contains`.
``` csharp
// Get controller scoped tag.
Tag controlelrTag = content.Get<Tag>("MyTagName")

// Get program scoped tag.
Tag programTag = content.Get<Tag>("/MyProgram/Tag/MyTagName")

// Find all tags with name (could be in multiple different programs).
IEnumerable<Tag> tags = content.Find<Tag>("MyTagName")

// Get a nested controller tag member
Tag tagMember = content.Get<Tag>("MyTag.Member[1].SubMember.1");

// Try get single tag.
var result = content.TryGet("/Tag/MyTagName", out var tag);
```

### Modify L5X Content

``` csharp
// Create and add a new tag
var newTag = new Tag { Name = "MyTag", Value = 100 };
content.Tags.Add(newTag);

// Remove a tag
content.Tags.Remove("OldTag");

// Modify tag properties
var tag = content.Tags.Find("ExistingTag");
tag.Value = 50;
tag.Description = "Updated tag description";
tag.ExternalAccess = ExternalAccess.ReadOnly;

// Bulk update components
content.Tags.Update(
    t => t.DataType == "TIMER", //update condition
    t => t.Description = "Updated TIMER description" //update action
);

// Save changes
content.Save("C:\\PathToMyOutputFile\\UpdatedFile.L5X");
```

## Component Types

L5Sharp provides support for all primary Logix components:
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

## Use Cases
- Automation of PLC development tasks
- Consistency checks across multiple PLC projects
- Tag documentation and reporting
- PLC code generation
- Automation tool development.

## Requirements
- .NET Standard 2.0 or .NET 8.0 compatible framework
- C# 12.0 support

## License
This project is licensed under the MIT License.

## Contributing
Contributions are welcome! If you find issues or have suggestions for improvements, 
please create an issue or submit a pull request to the [GitHub repository](https://github.com/tnunnink/L5Sharp).

## Feedback
If you find this library useful, please consider leaving a star on the GitHub repository. 
Any feedback or questions can be submitted via the [issues](https://github.com/tnunnink/L5Sharp/issues) section.

