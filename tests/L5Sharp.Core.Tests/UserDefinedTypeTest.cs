using System.Linq;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class UserDefinedTypeTest : DataType
    {
        public UserDefinedTypeTest() : base(nameof(UserDefinedTypeTest), "My Type description")
        {
            AddMember(nameof(MyMember01), Predefined.Bool, "This is a test member");
            AddMember(nameof(MyMember02), Predefined.Dint, "This is a test member array", 5, Radix.Ascii);
        }

        public IMember MyMember01 => Members.SingleOrDefault(m => m.Name == nameof(MyMember01));
        public IMember MyMember02 => Members.SingleOrDefault(m => m.Name == nameof(MyMember02));


        [Test]
        public void ShouldBeInitializedAsExpected()
        {
            Name.Should().Be("UserDefinedTypeTest");
            Description.Should().Be("My Type description");
            Family.Should().Be(DataTypeFamily.None);
            Class.Should().Be(DataTypeClass.User);

            MyMember01.Should().NotBeNull();
            MyMember01.DataType.Should().Be(Predefined.Bool);
            MyMember01.Description.Should().Be("This is a test member");
            MyMember01.Dimension.Should().Be(0);
            MyMember01.Radix.Should().Be(Radix.Decimal);
            MyMember01.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);

            MyMember02.Should().NotBeNull();
            MyMember02.DataType.Should().Be(Predefined.Dint);
            MyMember02.Description.Should().Be("This is a test member array");
            MyMember02.Dimension.Should().Be(5);
            MyMember02.Radix.Should().Be(Radix.Ascii);
            MyMember02.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        }
    }
}