﻿using FluentAssertions;


namespace L5Sharp.Tests;

[TestFixture]
public class L5XReferenceTests
{
    #region TestFile
    
    [Test]
    public void References_KnownTagWithReferences_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var references = content.References(Known.Tag).ToList();

        references.Should().NotBeEmpty();
    }

    [Test]
    public void References_FromKnownTagInstanceWithReferences_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);
        var tag = content.Get<Tag>(Known.Tag);

        var references = tag.References().ToList();

        references.Should().NotBeEmpty();
    }

    [Test]
    public void References_AgainstAllTags_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Example, L5XOptions.Index);

        var tags = content.Query<Tag>().ToList();

        var references = tags.Select(t => new { t.TagName, Refernces = t.References() }).ToList();

        references.Should().NotBeEmpty();
    }

    [Test]
    public void References_KnownDataType_ShouldReturnElementsWithExpectedDataType()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);
        var dataType = content.DataTypes.Get(Known.DataType);

        var references = dataType.References().ToList();

        references.Should().NotBeEmpty();
    }

    [Test]
    public void References_KnownInstruction_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);
        
        var instruction = content.Get<AddOnInstruction>(Known.AddOnInstruction);

        var references = instruction.References().ToList();

        references.Should().NotBeEmpty();
        
    }

    [Test]
    public void References_AllDataTypes_DoesThatWork()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var references = content.DataTypes.Select(d => new { d.Name, References = d.References().ToList() }).ToList();

        references.Should().NotBeEmpty();
    }

    #endregion

    #region ExampleFile

    [Test]
    public void References_ExampleDataType_ShouldHaveNoUnused()
    {
        var content = L5X.Load(Known.Example, L5XOptions.Index);

        var unused = content.Query<DataType>()
            .Select(d => new { d.Name, References = d.References().ToList() })
            .Where(d => d.References.Count == 0)
            .ToList();

        unused.Should().NotBeEmpty();
    }

    

    #endregion
}