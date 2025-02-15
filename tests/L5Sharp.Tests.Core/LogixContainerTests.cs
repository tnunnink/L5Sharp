using FluentAssertions;

// ReSharper disable CollectionNeverUpdated.Local

namespace L5Sharp.Tests.Core;

[TestFixture]
public class LogixContainerTests
{
    [Test]
    public void New_Default_ShouldBeEmpty()
    {
        var collection = new LogixContainer<TestElement>();

        collection.Should().NotBeNull();
        collection.Should().BeEmpty();
    }

    [Test]
    public void New_CollectionOverload_ShouldHaveExpectedCount()
    {
        var collection = new LogixContainer<TestElement>([new TestElement(), new TestElement(), new TestElement()]);

        collection.Should().HaveCount(3);
        collection.Count.Should().Be(3);
    }
}