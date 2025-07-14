using FluentAssertions;

namespace L5Sharp.Tests.Core;

[TestFixture]
public class LogixLookupTests
{
    [Test]
    public void Contains_KnownElement_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains(Reference.To<Tag>(Known.Tag));

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_NonExisting_ShouldBeFalse()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains("/Tag/FakeTag");

        result.Should().BeFalse();
    }

    [Test]
    public void Contains_KnownElementWithBuilder_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains(x => x.Tag(Known.Tag));

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_NonExistingWithBuilder_ShouldBeFalse()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Contains(x => x.Tag("FakeTag"));

        result.Should().BeFalse();
    }

    [Test]
    public void Get_KnownTagByReference_ShouldBeExpectedElement()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get(Reference.To<Tag>(Known.Tag));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_NonExistingReference_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Get<Tag>("FakeTag")).Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void Get_NullReference_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Get((Reference)null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Get_EmptyReference_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Get(string.Empty)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Get_TypeAndName_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get<Tag>(Known.Tag);

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_NonExistingName_ShouldThrowException()
    {
        var content = L5X.Load(Known.Test);

        FluentActions.Invoking(() => content.Get<Tag>("FakeTag")).Should().Throw<KeyNotFoundException>();
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
    public void Get_BuilderWithProgramTypeNamePath_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get(x => x.Tag(Known.Tag).In("MainProgram"));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.As<Tag>().Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_TypedBuilderWithTypeNamePath_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get<Tag>(x => x.Named(Known.Tag));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.Name.Should().Be(Known.Tag);
    }

    [Test]
    public void Get_TypedBuilderWithProgramTypeNamePath_ShouldBeExpected()
    {
        var content = L5X.Load(Known.Test);

        var result = content.Get<Tag>(x => x.Named(Known.Tag).In("MainProgram"));

        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
        result.Name.Should().Be(Known.Tag);
    }

    [Test]
    public void TryGet_NoTypeSpecified_ShouldBeFalse()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet(Known.DataType, out var entity);

        result.Should().BeFalse();
        entity.Should().BeNull();
    }
    
    [Test]
    public void TryGet_KnownType_ShouldBeTrue()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet(Reference.To<DataType>(Known.DataType), out var entity);

        result.Should().BeTrue();
        entity.Should().NotBeNull();
        entity.As<DataType>().Name.Should().Be(Known.DataType);
    }

    [Test]
    public void TryGet_TypedKnownName_ShouldBeTrueAndExpectedComponent()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet<DataType>(Known.DataType, out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.Name.Should().Be(Known.DataType);
    }

    [Test]
    public void TryGet_BuilderKnownDataTypeElement_ShouldBeExpectedTypeAndName()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet(s => s.DataType(Known.DataType), out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.As<DataType>().Name.Should().Be(Known.DataType);
    }

    [Test]
    public void TryGet_TypedBuilderKnownDataTypeElement_ShouldBeExpectedTypeAndName()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet<DataType>(s => s.Named(Known.DataType), out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.Name.Should().Be(Known.DataType);
    }

    [Test]
    public void TryGet_BuilderKnownTagElement_ShouldBeExpectedTypeAndName()
    {
        var content = L5X.Load(Known.Test);

        var result = content.TryGet(s => s.Tag(Known.Tag), out var component);

        result.Should().BeTrue();
        component.Should().NotBeNull();
        component.As<Tag>().Name.Should().Be(Known.Tag);
        
    }
}