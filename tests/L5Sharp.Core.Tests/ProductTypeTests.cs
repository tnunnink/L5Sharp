using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ProductTypeTests
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
            var productType = new ProductType(_fixture.Create<ushort>());

            productType.Should().NotBeNull();
        }
        
        [Test]
        public void New_Overloaded_ShouldNotBeNull()
        {
            var productType = new ProductType(_fixture.Create<ushort>(), _fixture.Create<string>());

            productType.Should().NotBeNull();
        }
        
        [Test]
        public void Id_GetValue_ShouldBeExpected()
        {
            var id = _fixture.Create<ushort>();
            var productType = new ProductType(id);

            productType.Id.Should().Be(id);
        }
        
        [Test]
        public void Name_GetValue_ShouldBeExpected()
        {
            var id = _fixture.Create<ushort>();
            var name = _fixture.Create<string>();
            var productType = new ProductType(id, name);

            productType.Name.Should().Be(name);
        }

        [Test]
        public void Unknown_WhenCalled_ShouldBeZero()
        {
            var productType = ProductType.Unknown;

            productType.Should().NotBeNull();
            productType.Id.Should().Be(0);
            productType.Name.Should().BeEmpty();
        }
        
        [Test]
        public void Discrete_WhenCalled_ShouldBeExpected()
        {
            var productType = ProductType.Discrete;

            productType.Should().NotBeNull();
            productType.Id.Should().Be(7);
            productType.Name.Should().Be("General Purpose Discrete I/O");
        }
        
        [Test]
        public void Analog_WhenCalled_ShouldBeExpected()
        {
            var productType = ProductType.Analog;

            productType.Should().NotBeNull();
            productType.Id.Should().Be(10);
            productType.Name.Should().Be("General Purpose Analog I/O");
        }
        
        [Test]
        public void Controller_WhenCalled_ShouldBeExpected()
        {
            var productType = ProductType.Controller;

            productType.Should().NotBeNull();
            productType.Id.Should().Be(14);
            productType.Name.Should().Be("Programmable Logic Controller");
        }
        
        [Test]
        public void Communications_WhenCalled_ShouldBeExpected()
        {
            var productType = ProductType.Communications;

            productType.Should().NotBeNull();
            productType.Id.Should().Be(12);
            productType.Name.Should().Be("Communications Adapter");
        }

        [Test]
        public void Parse_ValidNumber_ShouldBeExpected()
        {
            var productType = ProductType.Parse("1");

            productType.Should().NotBeNull();
            productType.Id.Should().Be(1);
            productType.Name.Should().BeEmpty();
        }
        
        [Test]
        public void Parse_InvalidNumber_ShouldBeUnknown()
        {
            var productType = ProductType.Parse("-11");

            productType.Should().Be(ProductType.Unknown);
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeId()
        {
            var id = _fixture.Create<ushort>();
            var name = _fixture.Create<string>();
            var productType = new ProductType(id, name);

            var value = productType.ToString();

            value.Should().Be(name);
        }

        [Test]
        public void ImplicitOperator_UshortToProductType_ShouldBeExpected()
        {
            var value = _fixture.Create<ushort>();
            
            ProductType number = value;
            
            number.Id.Should().Be(value);
        }
        
        [Test]
        public void ImplicitOperator_ProductTypeToUshort_ShouldBeExpected()
        {
            var value = new ProductType(_fixture.Create<ushort>());
            
            ushort number = value;
            
            number.Should().Be(value);
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var value = _fixture.Create<ushort>();
            var first = new ProductType(value);
            var second = new ProductType(value);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var value = _fixture.Create<ushort>();
            var first = new ProductType(value);

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var value = _fixture.Create<ushort>();
            var first = new ProductType(value);

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var value = _fixture.Create<ushort>();
            var first = new ProductType(value);
            var second = new ProductType(value);

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var value = _fixture.Create<ushort>();
            var first = new ProductType(value);

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var value = _fixture.Create<ushort>();
            var first = new ProductType(value);

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var value = _fixture.Create<ushort>();
            var first = new ProductType(value);
            var second = new ProductType(value);

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var value = _fixture.Create<ushort>();
            var first = new ProductType(value);
            var second = new ProductType(value);

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var value = _fixture.Create<ushort>();
            var first = new ProductType(value);

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}