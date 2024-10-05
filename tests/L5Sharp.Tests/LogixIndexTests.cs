using FluentAssertions;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixIndexTests
{
     [Test]
    public void Contains_KnownElementRelativeScope_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.Contains($"/Tag/{Known.Tag}");

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_KnownElementAbsoluteScope_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.Contains($"TestController/Tag/{Known.Tag}");

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_NonExistingRelativeScope_ShouldBeFalse()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.Contains("/Tag/FakeTag");

        result.Should().BeFalse();
    }

    [Test]
    public void Contains_KnownElementScopeBuilder_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.Contains(x => x.Tag(Known.Tag));

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_NonExistingScopeBuilder_ShouldBeFalse()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.Contains(x => x.Tag("FakeTag"));

        result.Should().BeFalse();
    }

    [Test]
    public void Find_NullScope_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        FluentActions.Invoking(() => content.Find(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Find_KnownElementNoType_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        FluentActions.Invoking(() => content.Find(Known.Tag)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Find_KnownElementRelativeScope_ShouldHaveExpectedCount()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var results = content.Find($"/Tag/{Known.Tag}");

        results.Should().HaveCount(2);
    }

    [Test]
    public void Find_TypeAndNullName_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        FluentActions.Invoking(() => content.Find<Tag>(null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Find_TypeAndEmptyName_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        FluentActions.Invoking(() => content.Find<Tag>(string.Empty)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Find_KnownElementTypeAndName_ShouldHaveExpectedCount()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var results = content.Find<Tag>(Known.Tag);

        results.Should().HaveCount(2);
    }

    [Test]
    public void Find_NonExistingTypeAndName_ShouldBeEmpty()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var results = content.Find<Tag>("FakeTagName");

        results.Should().BeEmpty();
    }

    [Test]
    public void Find_KnownTagMemberTypeAndName_ShouldNotBeEmpty()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var results = content.Find<Tag>("TestSimpleTag.DintMember").ToList();

        results.Should().HaveCount(2);
        results.Should().AllSatisfy(t => t.TagName.Should().Be("TestSimpleTag.DintMember"));
    }

    [Test]
    public void Get_ScopeRelative_ShouldBeExpectedElement()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.Get($"/Tag/{Known.Tag}");

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_ScopeAbsolute_ShouldBeExpectedElement()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.Get($"TestController/Tag/{Known.Tag}");

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_NonExistingScope_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        FluentActions.Invoking(() => content.Get("/Tag/FakeTag")).Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void Get_NullScope_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        FluentActions.Invoking(() => content.Get((Scope)null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Get_EmptyScope_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        FluentActions.Invoking(() => content.Get(string.Empty)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Get_TypeAndName_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.Get<Tag>(Known.Tag);

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_NonExistingName_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        FluentActions.Invoking(() => content.Get<Tag>("FakeTag")).Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void Get_BuilderWithTypeNamePath_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.Get(x => x.Tag(Known.Tag));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }
    
    [Test]
    public void Get_BuilderWithProgramTypeNamePath_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.Get(x => x.In("MainProgram").Tag(Known.Tag));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }
    
    [Test]
    public void Get_TypedBuilderWithTypeNamePath_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.Get<Tag>(x => x.Tag(Known.Tag));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.Name.Should().Be(Known.Tag);
    }
    
    [Test]
    public void Get_TypedBuilderWithProgramTypeNamePath_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.Get<Tag>(x => x.In("MainProgram").Tag(Known.Tag));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.Name.Should().Be(Known.Tag);
    }

    [Test]
    public void TryGet_KnownNameNoType_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        FluentActions.Invoking(() => content.TryGet(Known.DataType, out _)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void TryGet_RelativeScope_ShouldBeTrueAndExpectedComponent()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.TryGet($"/DataType/{Known.DataType}", out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.As<DataType>().Name.Should().Be(Known.DataType);
    }
    
    [Test]
    public void TryGet_TypedKnownName_ShouldBeTrueAndExpectedComponent()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.TryGet<DataType>(Known.DataType, out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.Name.Should().Be(Known.DataType);
    }

    [Test]
    public void TryGet_BuilderKnownDataTypeElement_ShouldBeExpectedTypeAndName()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.TryGet(s => s.DataType(Known.DataType), out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.As<DataType>().Name.Should().Be(Known.DataType);
    }
    
    [Test]
    public void TryGet_TypedBuilderKnownDataTypeElement_ShouldBeExpectedTypeAndName()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.TryGet<DataType>(s => s.DataType(Known.DataType), out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.Name.Should().Be(Known.DataType);
    }

    [Test]
    public void TryGet_BuilderKnownTagElement_ShouldBeExpectedTypeAndName()
    {
        var content = L5X.Load(Known.Test, L5XOptions.Index);

        var result = content.TryGet(s => s.Tag(Known.Tag), out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.As<Tag>().Name.Should().Be(Known.Tag);
    }
}