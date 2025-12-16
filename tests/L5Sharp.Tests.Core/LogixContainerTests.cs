using FluentAssertions;

// ReSharper disable CollectionNeverUpdated.Local

namespace L5Sharp.Tests.Core;

[TestFixture]
public class LogixContainerTests
{
    [Test]
    public void New_Default_ShouldBeEmpty()
    {
        var container = new LogixContainer<TestElement>();

        container.Should().NotBeNull();
        container.Should().BeEmpty();
    }

    [Test]
    public void New_CollectionOverload_ShouldHaveExpectedCount()
    {
        var container = new LogixContainer<TestElement>([new TestElement(), new TestElement(), new TestElement()]);

        container.Should().HaveCount(3);
        container.Count.Should().Be(3);
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

    [Test]
    public void AddRange_ValidElements_ShouldHaveExpectedCount()
    {
        var container = new LogixContainer<TestElement>();

        container.AddRange([
            new TestElement { RequiredValue = "first" },
            new TestElement { RequiredValue = "second" },
            new TestElement { RequiredValue = "third" }
        ]);

        container.Should().HaveCount(3);
    }

    [Test]
    public Task AddRange_ValidElements_ShouldBeVerified()
    {
        var container = new LogixContainer<TestElement>();

        container.AddRange([
            new TestElement { RequiredValue = "first" },
            new TestElement { RequiredValue = "second" },
            new TestElement { RequiredValue = "third" }
        ]);

        return VerifyXml(container.Serialize().ToString());
    }

    [Test]
    public void AddRange_NullCollection_ShouldThrowException()
    {
        var container = new LogixContainer<TestElement>();

        FluentActions.Invoking(() => container.AddRange(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void AddRange_NullObject_ShouldHaveExpectedCount()
    {
        var container = new LogixContainer<TestElement>();

        container.AddRange([
            new TestElement { RequiredValue = "first" },
            null!,
            new TestElement { RequiredValue = "third" }
        ]);

        container.Should().HaveCount(2);
    }

    [Test]
    public void Clear_NoElements_ShouldBeEmpty()
    {
        var container = new LogixContainer<TestElement>();

        container.Clear();

        container.Should().BeEmpty();
    }

    [Test]
    public void Clear_WithElements_ShouldBeEmpty()
    {
        LogixContainer<TestElement> container = [new(), new(), new()];

        container.Clear();

        container.Should().BeEmpty();
    }

    [Test]
    public void Contains_NoElement_ShouldBeFalse()
    {
        LogixContainer<TestElement> container = [];

        var result = container.Contains(new TestElement());

        result.Should().BeFalse();
    }

    [Test]
    public void Contains_HasElementsButNotOneWeProvide_ShouldBeFalse()
    {
        LogixContainer<TestElement> container = [new(), new(), new()];

        var result = container.Contains(new TestElement());

        result.Should().BeFalse();
    }

    [Test]
    public void Contains_HasElementWeProvide_ShouldBeTrue()
    {
        var target = new TestElement();
        LogixContainer<TestElement> container = [new(), target, new()];

        var result = container.Contains(target);

        result.Should().BeTrue();
    }

    [Test]
    public void Any_ContainsElementWithCondition_ShouldBeTrue()
    {
        var target = new TestElement { Description = "This is a test" };
        LogixContainer<TestElement> container = [new(), target, new()];

        var result = container.Any(x => x.Description?.Contains("test") is true);

        result.Should().BeTrue();
    }

    [Test]
    public void CopyTo_EmptyCollection_ShouldAllBeNull()
    {
        LogixContainer<TestElement> container = [];
        var array = new TestElement[10];

        container.CopyTo(array, 0);

        array.Should().AllSatisfy(x => x.Should().BeNull());
    }

    [Test]
    public void CopyTo_MatchingSize_ShouldBeExpectedSizeAndHaveNonNullInstance()
    {
        LogixContainer<TestElement> container = [new(), new(), new()];
        var array = new TestElement[3];

        container.CopyTo(array, 0);

        array.Should().AllSatisfy(x => x.Should().NotBeNull());
        array.Should().HaveCount(3);
    }

    [Test]
    public void CopyTo_NonZeroIndex_Should()
    {
        LogixContainer<TestElement> container = [new(), new(), new()];
        var array = new TestElement[3];

        container.CopyTo(array, 1);

        array.Should().AllSatisfy(x => x.Should().NotBeNull());
    }

    [Test]
    public void Remove_NoElements_ShouldReturnFalseAndBeEmpty()
    {
        LogixContainer<TestElement> container = [];

        var result = container.Remove(new TestElement());

        result.Should().BeFalse();
        container.Should().BeEmpty();
    }

    [Test]
    public void Remove_ManyElementsWithoutTargetElement_ShouldBeFalseAndHaveExpectedCount()
    {
        LogixContainer<TestElement> container = [new(), new(), new()];

        var result = container.Remove(new TestElement());

        result.Should().BeFalse();
        container.Should().HaveCount(3);
    }
    
    [Test]
    public void Remove_ManyElementsWithTargetElement_ShouldBeTrueAndHaveExpectedCount()
    {
        var target = new TestElement { Description = "This is a test" };
        LogixContainer<TestElement> container = [new(), target, new()];

        var result = container.Remove(target);

        result.Should().BeTrue();
        container.Should().HaveCount(2);
    }
}