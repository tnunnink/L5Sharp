using System.Linq;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class UserDefinedTypeTest : DataType
    {
        public UserDefinedTypeTest() : base(nameof(UserDefinedTypeTest), description: "My Type description")
        {
            Members.Add(MyMember01);
            Members.Add(MyMember02);
        }

        public IMember<IDataType> MyMember01 =
            Member.Create(nameof(MyMember01), new Bool(), description: "This is a test member");

        public IMember<IDataType> MyMember02 = Member.Create(nameof(MyMember02), new Dint(), new Dimensions(5),
            Radix.Ascii,
            description: "This is a test member array");


        [Test]
        public void ShouldBeInitializedAsExpected()
        {
            Name.ToString().Should().Be("UserDefinedTypeTest");
            Description.Should().Be("My Type description");
            Family.Should().Be(DataTypeFamily.None);
            Class.Should().Be(DataTypeClass.User);

            MyMember01.Should().NotBeNull();
            MyMember01.DataType.Should().Be(new Bool());
            MyMember01.Description.Should().Be("This is a test member");
            MyMember01.Dimension.Length.Should().Be(0);
            MyMember01.Radix.Should().Be(Radix.Decimal);
            MyMember01.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);

            MyMember02.Should().NotBeNull();
            MyMember02.DataType.Should().Be(new Dint(Radix.Ascii));
            MyMember02.Description.Should().Be("This is a test member array");
            MyMember02.Dimension.Length.Should().Be(5);
            MyMember02.Radix.Should().Be(Radix.Ascii);
            MyMember02.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        }
    }
}