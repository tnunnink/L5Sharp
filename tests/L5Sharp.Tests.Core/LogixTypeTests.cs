using FluentAssertions;
using L5Sharp.Tests.Core.Data.Custom;

namespace L5Sharp.Tests.Core;

[TestFixture]
public class LogixTypeTests
{
    [Test]
    public void Null_WhenCalled_ShouldBeExpected()
    {
        var type = LogixType.Null;

        type.Should().NotBeNull();
        type.Name.Should().Be("NULL");
        type.Members.Should().BeEmpty();
    }

    [Test]
    public void Register_ValidType_ShouldBeRegistered()
    {
        LogixType.Register("MyTypeName", () => new MyUnregisteredType());

        var result = LogixType.IsRegistered("MyTypeName");

        result.Should().BeTrue();
    }

    [Test]
    public void Register_EmptyKey_ShouldThrowException()
    {
        FluentActions.Invoking(() => LogixType.Register(string.Empty, () => new MySimpleData()))
            .Should().Throw<ArgumentException>();
    }

    [Test]
    public void Register_NullFactory_ShouldThrowException()
    {
        FluentActions.Invoking(() => LogixType.Register<MySimpleData>("MyType", null!))
            .Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Create_BOOL_ShouldBeExpected()
    {
        var type = LogixType.Create("BOOL");

        type.Should().NotBeNull();
        type.Should().BeOfType<BOOL>();
        type.Should().Be(false);
    }

    /*[Test]
    public void Create_BIT_ShouldBeExpected()
    {
        var type = LogixType.Create("BIT");

        type.Should().NotBeNull();
        type.Should().BeOfType<BOOL>();
        type.Should().BeEquivalentTo(new BOOL());
    }*/

    [Test]
    public void Create_SINT_ShouldBeExpected()
    {
        var type = LogixType.Create("SINT");

        type.Should().NotBeNull();
        type.Should().BeOfType<SINT>();
        type.Should().Be(0);
    }

    [Test]
    public void Create_INT_ShouldBeExpected()
    {
        var type = LogixType.Create("INT");

        type.Should().NotBeNull();
        type.Should().BeOfType<INT>();
        type.Should().BeEquivalentTo(new INT());
        type.Should().Be(0);
    }

    [Test]
    public void Create_DINT_ShouldBeExpected()
    {
        var type = LogixType.Create("DINT");

        type.Should().NotBeNull();
        type.Should().BeOfType<DINT>();
        type.Should().BeEquivalentTo(new DINT());
        type.Should().Be(0);
    }

    [Test]
    public void Create_LINT_ShouldBeExpected()
    {
        var type = LogixType.Create("LINT");

        type.Should().NotBeNull();
        type.Should().BeOfType<LINT>();
        type.Should().Be(0);
    }

    [Test]
    public void Create_REAL_ShouldBeExpected()
    {
        var type = LogixType.Create("REAL");

        type.Should().NotBeNull();
        type.Should().BeOfType<REAL>();
        type.Should().Be(0);
    }

    [Test]
    public void Create_TIMER_ShouldBeExpected()
    {
        var type = LogixType.Create("TIMER");

        type.Should().NotBeNull();
        type.Should().BeOfType<TIMER>();
        type.Members.Should().HaveCount(5);
    }

    [Test]
    public void Create_NonRegistered_ShouldThrowException()
    {
        FluentActions.Invoking(() => LogixType.Create("Fake"))
            .Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Create_RegisteredGenericOverload_ShouldBeNotNullAndExpectedType()
    {
        var type = LogixType.Create<BOOL>();

        type.Should().NotBeNull();
        type.Should().BeOfType<BOOL>();
        type.Should().Be(false);
    }

    [Test]
    public void IsRegistered_RegisteredType_ShouldBeTrue()
    {
        var result = LogixType.IsRegistered("TIMER");

        result.Should().BeTrue();
    }

    [Test]
    public void IsRegistered_FakeType_ShouldBeFalse()
    {
        var result = LogixType.IsRegistered("FAKE");

        result.Should().BeFalse();
    }

    [Test]
    [TestCase("BOOL")]
    [TestCase("SINT")]
    [TestCase("INT")]
    [TestCase("DINT")]
    [TestCase("LINT")]
    [TestCase("USINT")]
    [TestCase("UINT")]
    [TestCase("UDINT")]
    [TestCase("ULINT")]
    [TestCase("REAL")]
    [TestCase("LREAL")]
    public void IsAtomic_AtomicType_ShouldBeTrue(string typeName)
    {
        var result = LogixType.IsAtomic(typeName);

        result.Should().BeTrue();
    }

    [Test]
    public void IsAtomic_StructureType_ShouldBeFalse()
    {
        var result = LogixType.IsAtomic(nameof(TIMER));

        result.Should().BeFalse();
    }

    [Test]
    [TestCase(typeof(BOOL), "BOOL")]
    [TestCase(typeof(SINT), "SINT")]
    [TestCase(typeof(INT), "INT")]
    [TestCase(typeof(DINT), "DINT")]
    [TestCase(typeof(LINT), "LINT")]
    [TestCase(typeof(USINT), "USINT")]
    [TestCase(typeof(UINT), "UINT")]
    [TestCase(typeof(UDINT), "UDINT")]
    [TestCase(typeof(ULINT), "ULINT")]
    [TestCase(typeof(REAL), "REAL")]
    [TestCase(typeof(LREAL), "LREAL")]
    [TestCase(typeof(TIMER), "TIMER")]
    public void NameFor_ValidType_ShouldBeExpected(Type type, string name)
    {
        var result = LogixType.NameFor(type);

        result.Should().Be(name);
    }
}