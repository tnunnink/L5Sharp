﻿using FluentAssertions;
using L5Sharp.Samples;

namespace L5Sharp.Tests;

[TestFixture]
public class L5XTasksTests
{
    [Test]
    public void ToList_WhenCalled_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var tasks = content.Tasks.ToList();

        tasks.Should().NotBeEmpty();
    }
    
    [Test]
    public void Index_ValidIndex_ShouldNotBeNull()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Tasks[0];

        result.Should().NotBeNull();
    }

    [Test]
    public void Index_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Tasks[100]).Should().Throw<ArgumentOutOfRangeException>();
    }
}