using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Tests.Core
{
    [TestFixture]
    public class VendorTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var vendor = new Vendor(_fixture.Create<ushort>());

            vendor.Should().NotBeNull();
        }

        [Test]
        public void New_Overloaded_ShouldNotBeNull()
        {
            var vendor = new Vendor(_fixture.Create<ushort>(), _fixture.Create<string>());

            vendor.Should().NotBeNull();
        }

        [Test]
        public void Id_GetValue_ShouldBeExpected()
        {
            var id = _fixture.Create<ushort>();
            var vendor = new Vendor(id);

            vendor.Id.Should().Be(id);
        }

        [Test]
        public void Name_GetValue_ShouldBeExpected()
        {
            var id = _fixture.Create<ushort>();
            var name = _fixture.Create<string>();
            var vendor = new Vendor(id, name);

            vendor.Name.Should().Be(name);
        }

        [Test]
        public void Unknown_WhenCalled_ShouldBeZero()
        {
            var vendor = Vendor.Unknown;

            vendor.Should().NotBeNull();
            vendor.Id.Should().Be(0);
            vendor.Name.Should().BeEmpty();
        }
        
        [Test]
        public void Rockwell_WhenCalled_ShouldBeZero()
        {
            var vendor = Vendor.Rockwell;

            vendor.Should().NotBeNull();
            vendor.Id.Should().Be(1);
            vendor.Name.Should().Be("Rockwell Automation/Allen-Bradley");
        }

        [Test]
        public void Parse_ValidNumber_ShouldBeExpected()
        {
            var vendor = Vendor.Parse("1");

            vendor.Should().NotBeNull();
            vendor.Id.Should().Be(1);
            vendor.Name.Should().BeEmpty();
        }

        [Test]
        public void Parse_InvalidNumber_ShouldBeUnknown()
        {
            var vendor = Vendor.Parse("-11");

            vendor.Should().Be(Vendor.Unknown);
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeName()
        {
            var id = _fixture.Create<ushort>();
            var name = _fixture.Create<string>();
            var vendor = new Vendor(id, name);

            var value = vendor.ToString();

            value.Should().Be(name);
        }

        [Test]
        public void ImplicitOperator_UshortToVendor_ShouldBeExpected()
        {
            var value = _fixture.Create<ushort>();

            Vendor number = value;

            number.Id.Should().Be(value);
        }

        [Test]
        public void ImplicitOperator_VendorToUshort_ShouldBeExpected()
        {
            var value = new Vendor(_fixture.Create<ushort>());

            ushort number = value;

            number.Should().Be(value);
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var value = _fixture.Create<ushort>();
            var first = new Vendor(value);
            var second = new Vendor(value);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var value = _fixture.Create<ushort>();
            var first = new Vendor(value);

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var value = _fixture.Create<ushort>();
            var first = new Vendor(value);

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var value = _fixture.Create<ushort>();
            var first = new Vendor(value);
            var second = new Vendor(value);

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var value = _fixture.Create<ushort>();
            var first = new Vendor(value);

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var value = _fixture.Create<ushort>();
            var first = new Vendor(value);

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var value = _fixture.Create<ushort>();
            var first = new Vendor(value);
            var second = new Vendor(value);

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var value = _fixture.Create<ushort>();
            var first = new Vendor(value);
            var second = new Vendor(value);

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var value = _fixture.Create<ushort>();
            var first = new Vendor(value);

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}