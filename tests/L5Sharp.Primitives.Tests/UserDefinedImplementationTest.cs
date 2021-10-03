using System.Linq;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Extensibility;
using NUnit.Framework;

namespace L5Sharp.Primitives.Tests
{
    [TestFixture]
    public class UserDefinedImplementationTest : UserDefinedType
    {
        public UserDefinedImplementationTest() : base(nameof(UserDefinedImplementationTest), "My Type description")
        {
            RegisterMember(nameof(MyMember01), DataType.Bool, "This is a test member");
            RegisterMember(nameof(MyMember02), DataType.Dint, "This is a test member array", 5, Radix.Ascii);
        }

        public IMember MyMember01 => Members.SingleOrDefault(m => m.Name == nameof(MyMember01));
        public IMember MyMember02 => Members.SingleOrDefault(m => m.Name == nameof(MyMember02));


        [Test]
        public void ShouldBeInitializedAsExpected()
        {
            Name.Should().Be("UserDefinedImplementationTest");
            Description.Should().Be("My Type description");
            Family.Should().Be(DataTypeFamily.None);
            Class.Should().Be(DataTypeClass.User);

            MyMember01.Should().NotBeNull();
            MyMember01.DataType.Should().Be(DataType.Bool);
            MyMember01.Description.Should().Be("This is a test member");
            MyMember01.Dimension.Should().Be(0);
            MyMember01.Radix.Should().Be(Radix.Decimal);
            MyMember01.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);

            MyMember02.Should().NotBeNull();
            MyMember02.DataType.Should().Be(DataType.Dint);
            MyMember02.Description.Should().Be("This is a test member array");
            MyMember02.Dimension.Should().Be(5);
            MyMember02.Radix.Should().Be(Radix.Ascii);
            MyMember02.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        }
    }
}