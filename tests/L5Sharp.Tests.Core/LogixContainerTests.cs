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
    public void Indexer_GetValidIndex_ShouldReturnElement()
    {
        LogixContainer<TestElement> container =
        [
            new() { RequiredValue = "a" },
            new() { RequiredValue = "b" },
            new() { RequiredValue = "c" }
        ];

        var second = container[1];

        second.RequiredValue.Should().Be("b");
    }

    [Test]
    public void Indexer_GetOutOfRange_ShouldThrow()
    {
        LogixContainer<TestElement> container = [new(), new()];

        FluentActions.Invoking(() => _ = container[2]).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Indexer_SetValidIndex_ShouldReplaceElement()
    {
        LogixContainer<TestElement> container =
        [
            new() { RequiredValue = "a" },
            new() { RequiredValue = "b" },
            new() { RequiredValue = "c" }
        ];

        container[1] = new TestElement { RequiredValue = "x" };

        container[1].RequiredValue.Should().Be("x");
        container.Should().HaveCount(3);
    }

    [Test]
    public void Indexer_Set_OutOfRange_ShouldThrow()
    {
        // ReSharper disable once CollectionNeverQueried.Local
        LogixContainer<TestElement> container = [new(), new()];

        FluentActions.Invoking(() => container[2] = new TestElement()).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Add_NullElement_ShouldNotChangeCount()
    {
        LogixContainer<TestElement> container = [];

        // ReSharper disable once AppendToCollectionExpression
        container.Add(null);

        container.Should().BeEmpty();
    }

    [Test]
    public void Add_DefaultType_ShouldHaveExpectedCount()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var container = new LogixContainer<TestElement>();

        container.Add(new TestElement());

        container.Should().HaveCount(1);
    }

    [Test]
    public Task Add_ValidType_ShouldBeVerified()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
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
        // Need an array large enough to copy starting at non-zero index
        var array = new TestElement[4];

        container.CopyTo(array, 1);

        array[0].Should().BeNull();
        array[1].Should().NotBeNull();
        array[2].Should().NotBeNull();
        array[3].Should().NotBeNull();
    }


    [Test]
    public void IndexOf_ElementInContainer_ShouldReturnExpectedIndex()
    {
        var first = new TestElement { RequiredValue = "first" };
        var second = new TestElement { RequiredValue = "second" };
        var third = new TestElement { RequiredValue = "third" };

        LogixContainer<TestElement> container = [first, second, third];

        var index = container.IndexOf(second);

        index.Should().Be(1);
    }

    [Test]
    public void IndexOf_ElementNotInContainer_ShouldReturnNegativeOne()
    {
        LogixContainer<TestElement> container = [new(), new()];

        var index = container.IndexOf(new TestElement { RequiredValue = "zzz" });

        index.Should().Be(-1);
    }

    [Test]
    public void Insert_ValidIndex_ShouldInsertBeforeIndex()
    {
        LogixContainer<TestElement> container =
        [
            new() { RequiredValue = "a" },
            new() { RequiredValue = "c" }
        ];

        container.Insert(1, new TestElement { RequiredValue = "b" });

        container.Should().HaveCount(3);
        container[1].RequiredValue.Should().Be("b");
    }

    [Test]
    public void Insert_NullElement_ShouldThrow()
    {
        // ReSharper disable once CollectionNeverQueried.Local
        LogixContainer<TestElement> container = [new(), new()];

        FluentActions.Invoking(() => container.Insert(1, null!))
            .Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Insert_IndexOutOfRange_ShouldThrow()
    {
        // ReSharper disable once CollectionNeverQueried.Local
        LogixContainer<TestElement> container = [new(), new()];

        FluentActions.Invoking(() => container.Insert(2, new TestElement()))
            .Should().Throw<ArgumentOutOfRangeException>();
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

    [Test]
    public void RemoveAt_ValidIndex_ShouldRemoveElement()
    {
        LogixContainer<TestElement> container =
        [
            new() { RequiredValue = "a" },
            new() { RequiredValue = "b" },
            new() { RequiredValue = "c" }
        ];

        container.RemoveAt(1);

        container.Should().HaveCount(2);
        container[1].RequiredValue.Should().Be("c");
    }

    [Test]
    public void RemoveAt_IndexOutOfRange_ShouldThrow()
    {
        // ReSharper disable once CollectionNeverQueried.Local
        LogixContainer<TestElement> container = [new(), new()];

        FluentActions.Invoking(() => container.RemoveAt(2))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void RemoveIf_NullPredicate_ShouldThrow()
    {
        LogixContainer<TestElement> container = [new(), new()];

        FluentActions.Invoking(() => container.RemoveIf(null!))
            .Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void RemoveIf_PredicateMatch_ShouldRemoveExpected()
    {
        LogixContainer<TestElement> container =
        [
            new() { RequiredValue = "keep" },
            new() { RequiredValue = "remove" },
            new() { RequiredValue = "keep" }
        ];

        container.RemoveIf(e => e.RequiredValue == "remove");

        container.Should().HaveCount(2);
        container.Any(e => e.RequiredValue == "remove").Should().BeFalse();
    }

    [Test]
    public void Update_NullAction_ShouldThrow()
    {
        LogixContainer<TestElement> container = [new(), new()];

        FluentActions.Invoking(() => container.Update(null!))
            .Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Update_AllElements_ShouldApplyChange()
    {
        LogixContainer<TestElement> container = [new(), new(), new()];

        container.Update(e => e.Description = "updated");

        container.All(e => e.Description == "updated").Should().BeTrue();
    }

    [Test]
    public void Update_WithNullAction_ShouldThrow()
    {
        LogixContainer<TestElement> container = [new(), new()];

        FluentActions.Invoking(() => container.Update(null!, _ => true))
            .Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Update_WithNullPredicate_ShouldThrow()
    {
        LogixContainer<TestElement> container = [new(), new()];

        FluentActions.Invoking(() => container.Update(_ => { }, null!))
            .Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Update_WithPredicate_ShouldApplyOnlyToMatches()
    {
        LogixContainer<TestElement> container =
        [
            new() { RequiredValue = "x" },
            new() { RequiredValue = "y" },
            new() { RequiredValue = "x" }
        ];

        container.Update(e => e.Description = "hit", e => e.RequiredValue == "x");

        container[0].Description.Should().Be("hit");
        container[1].Description.Should().BeNull();
        container[2].Description.Should().Be("hit");
    }

    [Test]
    public void Enumerator_WhenIterated_ShouldYieldAllElements()
    {
        LogixContainer<TestElement> container = [new(), new(), new()];
        var count = 0;
        foreach (var _ in container) count++;

        count.Should().Be(3);
    }

    [Test]
    public void ICollection_CopyTo_ShouldCopyElements()
    {
        LogixContainer<TestElement> container = [new(), new(), new()];
        Array array = new TestElement[3];

        ((System.Collections.ICollection)container).CopyTo(array, 0);

        array.Cast<TestElement>().All(e => e is not null).Should().BeTrue();
    }

    [Test]
    public void ICollection_Properties_ShouldBeExpected()
    {
        LogixContainer<TestElement> container = [];

        ((ICollection<TestElement>)container).IsReadOnly.Should().BeFalse();
        System.Collections.ICollection col = container;
        col.IsSynchronized.Should().BeTrue();
        col.SyncRoot.Should().NotBeNull();
    }
}