using FluentAssertions;
using LogixHelper.Enumerations;
using LogixHelper.Exceptions;
using LogixHelper.Primitives;
using NUnit.Framework;

namespace LogixHelper.Tests
{
    [TestFixture]
    public class DataTypeMemberTests
    {
        /*[Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var member = new DataTypeMember("Member_Test_001", DataType.Bool);

            member.Should().NotBeNull();
        }

        [Test]
        public void New_InvalidName_ShouldThrowException()
        {
            FluentActions.Invoking(() => new DataTypeMember(string.Empty, DataType.Bool)).Should()
                .Throw<InvalidTagNameException>();
        }

        [Test]
        public void New_ValidName_ShouldHaveExpectedDefaults()
        {
            var member = new DataTypeMember("Member_Test_001", DataType.Bool);

            member.Name.Should().Be("Member_Test_001");
            member.DataType.Should().Be(DataType.Bool);
            member.Dimension.Should().Be(0);
            member.Radix.Should().Be(Radix.Decimal);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member.Description.Should().Be(string.Empty);
        }
        
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var member = new DataTypeMember("Member", DataType.Dint);

            var element = member.Serialize();

            element.Should().NotBeNull();
        }*/
    }
}