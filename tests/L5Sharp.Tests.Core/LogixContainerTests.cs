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

    [Test]
    public void Add_DefaultType_ShouldHaveExpectedCount()
    {
        var container = new LogixContainer<TestElement>();

        container.Add(new TestElement());

        container.Should().HaveCount(1);
    }

    [Test]
    public Task Add_ValidType_ShouldBeVerified()
    {
        var container = new LogixContainer<TestElement>();

        container.Add(new TestElement());

        return Verify(container.Serialize());
    }
}