using FluentAssertions;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixLookupTests
{
    [Test]
    public void Contains_KnownElementRelativeScope_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains($"/Tag/{Known.Tag}");

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_KnownElementAbsoluteScope_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains($"TestController/Tag/{Known.Tag}");

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_NonExistingRelativeScope_ShouldBeFalse()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains("/Tag/FakeTag");

        result.Should().BeFalse();
    }

    [Test]
    public void Contains_KnownElementScopeBuilder_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains(x => x.Tag(Known.Tag));

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_NonExistingScopeBuilder_ShouldBeFalse()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains(x => x.Tag("FakeTag"));

        result.Should().BeFalse();
    }

    [Test]
    public void Find_NullScope_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Find(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Find_KnownElementRelativeScope_ShouldHaveExpectedCount()
    {
        var content = L5X.Load(Known.Test);

        var results = content.Find($"/Tag/{Known.Tag}");

        results.Should().HaveCount(2);
    }

    [Test]
    public void Find_TypeAndNullName_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Find<Tag>(null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Find_TypeAndEmptyName_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Find<Tag>(string.Empty)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Find_KnownElementNoType_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Find(Known.Tag)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Find_KnownElementTypeAndName_ShouldHaveExpectedCount()
    {
        var content = L5X.Load(Known.Test);

        var results = content.Find<Tag>(Known.Tag);

        results.Should().HaveCount(2);
    }

    [Test]
    public void Find_NonExistingTypeAndName_ShouldBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var results = content.Find<Tag>("FakeTagName");

        results.Should().BeEmpty();
    }

    [Test]
    public void Find_KnownTagMemberTypeAndName_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test);

        var results = content.Find<Tag>("TestSimpleTag.DintMember").ToList();

        results.Should().NotBeEmpty();
        results.Should().AllSatisfy(t => t.TagName.Should().Be("TestSimpleTag.DintMember"));
    }

    [Test]
    public void Get_KnownElementTypeAndName_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get<Tag>(Known.Tag);

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_BuilderWithTypeNamePath_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get(x => x.Tag(Known.Tag));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void TryGet_ValidKey_ShouldBeExpectedTypeAndName()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet(s => s.DataType(Known.DataType), out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.As<DataType>().Name.Should().Be(Known.DataType);
    }

    [Test]
    public void TryGet_ValidKeyAndContainer_ShouldBeExpectedTypeAndName()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet(s => s.Tag(Known.Tag), out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void TryGet_ValidComponent_ShouldBeExpectedName()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet(Known.DataType, out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.As<DataType>().Name.Should().Be(Known.DataType);
    }

    [Test]
    public void Find_ValidTagName_ShouldNotBeNull()
    {
        var content = L5X.Load(Known.Test);

        var tag = content.Find<Tag>(Known.Tag);

        tag.Should().NotBeNull();
    }

    [Test]
    public void ReferencesTo_ValidComponent_ShouldHaveExpectedCount()
    {
        var content = L5X.Load(Known.Test);
        var tag = content.Get(Known.Tag).As<Tag>();

        var references = content.References(tag.Name).ToList();

        references.Should().NotBeEmpty();
    }
}