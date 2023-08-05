using FluentAssertions;
using L5Sharp.Extensions.Tests.TestTypes;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Extensions.Tests;

[TestFixture]
public class LogixTypeExtensionsTests
{
    [Test]
    public void BuildUserTypes_ValidUserType_ShouldWork()
    {
        var type = new ComplexType("Test", new List<LogixMember>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 }),
            new("UserType", new MySimpleType())
        });

        var userTypes = type.ToUDT().ToList();

        userTypes.Should().HaveCount(1);
    }
}