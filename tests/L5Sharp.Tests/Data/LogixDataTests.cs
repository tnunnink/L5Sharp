using FluentAssertions;

namespace L5Sharp.Tests.Data;

[TestFixture]
public class LogixDataTests
{
    [Test]
    public void Create_BOOL_ShouldBeExpected()
    {
        var type = LogixData.Create("BOOL");
        
        type.Should().NotBeNull();
        type.Should().BeOfType<BOOL>();
        type.Should().BeEquivalentTo(new BOOL());
    }
    
    [Test]
    public void Create_BIT_ShouldBeExpected()
    {
        var type = LogixData.Create("BIT");
        
        type.Should().NotBeNull();
        type.Should().BeOfType<BOOL>();
        type.Should().BeEquivalentTo(new BOOL());
    }
    
    [Test]
    public void Create_SINT_ShouldBeExpected()
    {
        var type = LogixData.Create("SINT");

        type.Should().NotBeNull();
        type.Should().BeOfType<SINT>();
        type.Should().BeEquivalentTo(new SINT());
    }
    
    [Test]
    public void Create_INT_ShouldBeExpected()
    {
        var type = LogixData.Create("INT");

        type.Should().NotBeNull();
        type.Should().BeOfType<INT>();
        type.Should().BeEquivalentTo(new INT());
    }

    [Test]
    public void Create_DINT_ShouldBeExpected()
    {
        var type = LogixData.Create("DINT");

        type.Should().NotBeNull();
        type.Should().BeOfType<DINT>();
        type.Should().BeEquivalentTo(new DINT());
    }
    
    [Test]
    public void Create_LINT_ShouldBeExpected()
    {
        var type = LogixData.Create("LINT");

        type.Should().NotBeNull();
        type.Should().BeOfType<LINT>();
        type.Should().BeEquivalentTo(new LINT());
    }
    
    [Test]
    public void Create_REAL_ShouldBeExpected()
    {
        var type = LogixData.Create("REAL");

        type.Should().NotBeNull();
        type.Should().BeOfType<REAL>();
        type.Should().BeEquivalentTo(new REAL());
    }
    
    [Test]
    public void Create_TIMER_ShouldBeExpected()
    {
        var type = LogixData.Create("TIMER");

        type.Should().NotBeNull();
        type.Should().BeOfType<TIMER>();
        type.Should().BeEquivalentTo(new TIMER());
    }
}