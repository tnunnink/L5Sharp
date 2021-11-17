using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ArrayMemberTests
    {
        [Test]
        public void Create_ValidDimensions_ShouldBeOfTypeIArrayMember()
        {
            var member = Member.Create<Dint>("Test", new Dimensions(10));

            member.Should().BeOfType<IArrayMember<Dint>>();
        }

        [Test]
        public void Index_Get_ShouldReturnValidElement()
        {
            var member = Member.Create<Dint>("Test", new Dimensions(10));

            var element = member[5];

            element.Should().NotBeNull();
            element.Name.Should().Be("[5]");
            element.DataType.Should().BeOfType<Dint>();
            element.Radix.Should().Be(Radix.Decimal);
            element.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            element.Description.Should().BeNull();
        }

        [Test]
        public void IterateCollection_ShouldBeExpectedNames()
        {
            var member = Member.Create<Dint>("Test", new Dimensions(10));

            foreach (var element in member)
            {
                element.Should().NotBeNull();
            }
        }
    }
}