using FluentAssertions;

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
    public void Create_RegisteredAtomic_ShouldNotBeNullAndExpectedType()
    {
        var type = LogixType.Create("BOOL");

        type.Should().NotBeNull();
        type.Should().BeOfType<BOOL>();
        type.Should().Be(false);
    }

    [Test]
    public void Create_RegisteredStructure_ShouldNotBeNullAndExpectedType()
    {
        var type = LogixType.Create("TIMER");

        type.Should().NotBeNull();
        type.Should().BeOfType<TIMER>();
    }

    [Test]
    public void Create_NonRegistered_ShouldThrowException()
    {
        FluentActions.Invoking(() => LogixType.Create("Fake")).Should().Throw<InvalidOperationException>();
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
    public void IsAtomic_StructureTYpe_ShouldBeFalse()
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