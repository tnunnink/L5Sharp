using FluentAssertions;

namespace L5Sharp.Tests.Core.Data;

[TestFixture]
public class NullDataTests
{
    [Test]
    public void HasExpectedValues()
    {
        var type = NullData.Instance;

        type.Name.Should().Be("NULL");
        type.Members.Should().BeEmpty();
    }

    [Test]
    public void Update_AtomicValue_ShouldThrowException()
    {
        var type = NullData.Instance;
        
        FluentActions.Invoking(() => type.UpdateData(new DINT(123))).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public Task SerializeShouldBeVerified()
    {
        var type = NullData.Instance;

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }
}